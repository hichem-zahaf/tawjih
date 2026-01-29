using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Diagnostics;

namespace UIAutomation
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void SetCursorPos(int X, int Y);
        // Import the necessary DLL for mouse event
        [DllImport("user32.dll", SetLastError = true)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
        // Define constants for mouse events
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        // Actions array
        private List<Dictionary<string, string>> actions;
        // Http listner varriables
        private HttpListener _listener;
        private bool _isListenerRunning = false;
        private bool _istcpListenerRunning = false;
        private string _prefix;
        // http client
        //private static readonly HttpClient httpClient = new HttpClient();
        private readonly HttpClient httpClient;
        // tcp listner variables
        private TcpListener _tcpListener;
        // Data Array
        private List<string> data = new List<string>();
        // Debug Variables
        // CHANGE TO FALSE IN PRODUCTION
        // This variable to enable debug mode, which will toggle api exception report
        public static bool UIA_debug = false;
        public static string UIA_debugUrl = "";
        public static string UIA_action;
        // Notify Icon and context menus
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenu;
        // Class level vars
        // Drag-related variables
        private bool isDragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        // End Variables
        public Form1()
        {
            InitializeComponent();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
            // Init Actions array
            LoadActionsFromFile();
            // Get debug value
            checkBoxDebug.Checked = UIA_debug;
            // Bypass SSL Certificate Authentication for .NET Framework 4.5.2
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            httpClient = new HttpClient();
        }
        private void HandleException(string message)
        {
            bool test = UIA_debug;
            if (!UIA_debug)
            {
                Uri result;
                if (!string.IsNullOrEmpty(UIA_debugUrl) && Uri.TryCreate(UIA_debugUrl, UriKind.Absolute, out result) &&
                    (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
                {
                    SendMessageAsync(UIA_debugUrl, $"Request error: {message}");
                }
            }
            else
            {
                MessageBox.Show($"(Source: HandleException): An error occurred  {message}");
            }
        }
        private void checkBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            UIA_debug = checkBoxDebug.Checked;
        }
        // Read actions array
        private void LoadActionsFromFile()
        {
            try
            {
                // Define the filename and get the path to the desktop
                string fileName = "actions.json";
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, fileName);

                // Read the file and deserialize the content into the actions list
                string jsonString = File.ReadAllText(filePath);
                actions = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading actions from file: " + ex.Message);
                actions = new List<Dictionary<string, string>>(); // Initialize to an empty list on error
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load predefined values
            SetupNotificationIcon();
            // Create Main Menu Items
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem editMenu = new ToolStripMenuItem("Tools");
            ToolStripMenuItem viewMenu = new ToolStripMenuItem("View");
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");
            //
            // Add Sub Menu Items for "File"
            ToolStripMenuItem loadItem = new ToolStripMenuItem("Load", null, LoadFile_Click);
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit", null, Exit_Click);
            fileMenu.DropDownItems.Add(loadItem);
            loadItem.Enabled = false;
            fileMenu.DropDownItems.Add(exitItem);
            //
            // Add Sub Menu Items for "Edit"
            ToolStripMenuItem startActionsItem = new ToolStripMenuItem("Start Automation", null, button10_Click);
            ToolStripMenuItem startHttpItem = new ToolStripMenuItem("HTTP Listner", null, HttpListener_Click);
            ToolStripMenuItem startTcpItem = new ToolStripMenuItem("TCP Listner", null, TcpListener_Click);
            editMenu.DropDownItems.Add(startActionsItem);
            editMenu.DropDownItems.Add(startHttpItem);
            editMenu.DropDownItems.Add(startTcpItem);
            startTcpItem.Enabled = false;
            //
            //
            // Add Sub Menu Items for "Help"
            ToolStripMenuItem termsItem = new ToolStripMenuItem("Terms and conditions", null, terns_Click);
            ToolStripMenuItem aboutItem = new ToolStripMenuItem("About", null, About_Click);
            helpMenu.DropDownItems.Add(termsItem);
            helpMenu.DropDownItems.Add(aboutItem);
            //
            // Add the Main Menu Items to the MenuStrip
            menuStrip2.Items.Add(fileMenu);
            menuStrip2.Items.Add(editMenu);
            menuStrip2.Items.Add(viewMenu);
            menuStrip2.Items.Add(helpMenu);
            //
            this.MainMenuStrip = menuStrip2;
            this.Controls.Add(menuStrip2);
            //
            this.KeyPreview = true;

        }
        // Find element using XPath
        private AutomationElement FindElementByXPath(AutomationElement rootElement, string xPath)
        {
            string[] pathSegments = xPath.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
            AutomationElement currentElement = rootElement;

            foreach (string segment in pathSegments)
            {
                TreeScope scope = segment.Contains('#') ? TreeScope.Children : TreeScope.Descendants;
                string cleanSegment = segment.Replace("#", "");

                string[] subSegments = segment.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string subSegment in subSegments)
                {
                    string[] parts = subSegment.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    string elementType = parts[0];
                    string condition = parts.Length > 1 ? parts[1] : null;

                    Condition typeCondition = GetTypeCondition(elementType);
                    Condition finalCondition = typeCondition;

                    if (!string.IsNullOrEmpty(condition))
                    {
                        Condition propertyCondition = GetPropertyCondition(condition);
                        finalCondition = new AndCondition(typeCondition, propertyCondition);
                    }
                    // TreeScope scope = (subSegment == subSegments[0] && pathSegments.Length > 1) ? TreeScope.Children : TreeScope.Descendants;
                    // TreeScope scope = TreeScope.Descendants;
                    try
                    {
                        if (currentElement == null)
                        {
                            return null;
                        }
                        currentElement = currentElement.FindFirst(scope, finalCondition);
                    }
                    catch (Exception ex)
                    {
                        HandleException($"Find Element Error: {ex.Message}");
                        return null;
                    }

                    if (currentElement == null)
                    {
                        return null;
                    }
                }
            }

            return currentElement;
        }
        // Get Type condition
        private Condition GetTypeCondition(string elementType)
        {
            switch (elementType.ToLower())
            {
                case "window":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                case "button":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
                case "edit":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit);
                case "table":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Table);
                case "text":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text);
                case "menuitem":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem);
                case "tree":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tree);
                case "treeitem":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem);
                case "pane":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane);
                case "Document":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Document);
                case "checkbox":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.CheckBox);
                case "radiobutton":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.RadioButton);
                case "combobox":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ComboBox);
                case "list":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.List);
                case "listitem":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem);
                case "tab":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tab);
                case "tabitem":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                case "hyperlink":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Hyperlink);
                case "custom":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom);
                case "header":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Header);
                case "headeritem":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.HeaderItem);
                case "splitbutton":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.SplitButton);
                case "menu":
                    return new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Menu);
                default:
                    return Condition.TrueCondition;
            }
        }
        // Get property Condition
        private Condition GetPropertyCondition(string condition)
        {
            string[] conditionParts = condition.Split('=');
            string property = conditionParts[0].Trim('@');
            string value = conditionParts[1].Trim('\'');
            int childId;
            int processId;

            switch (property.ToLower())
            {
                case "id":
                    return new PropertyCondition(AutomationElement.AutomationIdProperty, value);
                case "name":
                    if (value.StartsWith("@"))
                    {
                        string partialValue = value.TrimStart('@');
                        return GetPartialNameCondition(partialValue);
                    }
                    else
                    {
                        // Exact match
                        return new PropertyCondition(AutomationElement.NameProperty, value);
                    }
                case "class":
                    return new PropertyCondition(AutomationElement.ClassNameProperty, value);
                case "frameworkid":
                    return new PropertyCondition(AutomationElement.FrameworkIdProperty, value);
                case "childid":
                    if (int.TryParse(value, out childId))
                    {
                        return new PropertyCondition(AutomationElement.RuntimeIdProperty, new int[] { childId });
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid child ID: {value}");
                    }
                case "processid":
                    if (int.TryParse(value, out processId))
                    {
                        return new PropertyCondition(AutomationElement.ProcessIdProperty, processId);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid process ID: {value}");
                    }
            }
            return Condition.TrueCondition;
        }
        private Condition GetPartialNameCondition(string partialValue)
        {
            string partialValueLower = partialValue.ToLower();
            Condition allElementsCondition = Condition.TrueCondition;
            var allElements = AutomationElement.RootElement.FindAll(TreeScope.Descendants, allElementsCondition);
            foreach (AutomationElement element in allElements)
            {
                string name = element.Current.Name;
                if (name != null && name.ToLower().Contains(partialValueLower))
                {
                    return new PropertyCondition(AutomationElement.NameProperty, name);
                }
            }
            return new PropertyCondition(AutomationElement.NameProperty, string.Empty);
        }
        // Click element
        public bool ClickElementByXPath(string windowXPath, string elementXPath)
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);

            }
            else
            {
                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    HandleException("Window not found");
                    return false;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
            }
            if (targetElement == null)
            {
                HandleException("Element not found");
                return false;
            }
            try
            {
                object patternObj;


                if (targetElement.TryGetCurrentPattern(InvokePattern.Pattern, out patternObj))
                {
                    InvokePattern invokePattern = patternObj as InvokePattern;
                    invokePattern?.Invoke();
                    return true;
                }
                else
                {
                    HandleException("Element does not support InvokePattern");
                    return false;
                }
            }
            catch (ElementNotAvailableException ex)
            {
                HandleException(ex.Message);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                HandleException(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
                return false;
            }

        }
        public bool WaitForElementByXPath(string windowXPath, string elementXPath, int timeoutSeconds)
        {
            /*
            int timeoutMilliseconds = timeoutSeconds * 1000;
            int checkIntervalMilliseconds = 250;
            int maxIterations = timeoutMilliseconds / checkIntervalMilliseconds;
            AutomationElement targetElement = null;

            for (int i = 0; i < maxIterations; i++)
            {
                AutomationElement rootElement = AutomationElement.RootElement;
                targetElement = string.IsNullOrEmpty(windowXPath)
                    ? FindElementByXPath(rootElement, elementXPath)
                    : FindElementByXPath(FindElementByXPath(rootElement, windowXPath), elementXPath);

                if (targetElement != null)
                {
                    return true;
                }
                System.Threading.Thread.Sleep(checkIntervalMilliseconds);
            }

            if (targetElement == null)
            {
                //
                HandleException("Element not found within the timeout period");
                //MessageBox.Show("Element not found within the timeout period");
                return false;
            }
            return false;
            */
            int timeoutMilliseconds = timeoutSeconds * 1000;
            int checkIntervalMilliseconds = 250;
            int maxIterations = timeoutMilliseconds / checkIntervalMilliseconds;
            AutomationElement targetElement = null;

            // Wrapping the entire search process in a try-catch
            try
            {
                for (int i = 0; i < maxIterations; i++)
                {
                    try
                    {
                        // Root element of the UI Automation tree
                        AutomationElement rootElement = AutomationElement.RootElement;

                        // If windowXPath is empty, search directly, otherwise search within the window first
                        targetElement = string.IsNullOrEmpty(windowXPath)
                            ? FindElementByXPath(rootElement, elementXPath)
                            : FindElementByXPath(FindElementByXPath(rootElement, windowXPath), elementXPath);
                        // If targetElement is found, return success
                        if (targetElement != null)
                        {
                            return true;
                        }
                    }
                    catch (NullReferenceException)
                    {
                    }
                    catch (Exception ex)
                    {
                        HandleException($"Error during element search: {ex.Message}");
                    }
                    System.Threading.Thread.Sleep(checkIntervalMilliseconds);
                }
                HandleException("Element not found within the timeout period.");
                return false;
            }
            catch (Exception ex)
            {
                HandleException($"Fatal error: {ex.Message}");
                return false;
            }
        }
        // Click element Button
        private void button1_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;

            ClickElementByXPath(windowXPath, elementXPath);
        }
        // Set value button
        private void button2_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;
            string newValue = textBoxNewValue.Text;

            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {
                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    MessageBox.Show("Window not found");
                    return;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    MessageBox.Show("Element not found");
                    return;
                }
            }
            try
            {
                // Try ValuePattern 
                object valuePatternObj;
                if (targetElement.TryGetCurrentPattern(ValuePattern.Pattern, out valuePatternObj))
                {
                    ValuePattern valuePattern = (ValuePattern)valuePatternObj;
                    valuePattern.SetValue(newValue);
                    return;
                }
                // Value pattern is not supported
                Console.WriteLine("Element does not support ValuePattern");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return;
            }
        }
        // Set Text Button
        private void button6_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;
            string newValue = textBoxNewValue.Text;

            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {

                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    MessageBox.Show("Window not found");
                    return;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    MessageBox.Show("Element not found");
                    return;
                }

            }

            try
            {

                // try TextPattern
                object textPatternObj;
                if (targetElement.TryGetCurrentPattern(TextPattern.Pattern, out textPatternObj))
                {
                    TextPattern textPattern = (TextPattern)textPatternObj;
                    var textRange = textPattern.DocumentRange;
                    textRange.Select();
                    SendKeys.SendWait(newValue);
                    return;
                }

                // Text pattern is not supported
                Console.WriteLine("Element does not support TextPattern");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return;
            }

        }
        // Get value funtion
        private bool getvaluefunction(string windowXPath, string elementXPath)
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {

                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    HandleException("Window not found");
                    //MessageBox.Show("Window not found");
                    return false;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    //MessageBox.Show("Element not found");
                    HandleException("Element not found");
                    return false;
                }

            }
            try
            {
                object valuePatternObj;
                if (targetElement.TryGetCurrentPattern(ValuePattern.Pattern, out valuePatternObj))
                {
                    ValuePattern valuePattern = (ValuePattern)valuePatternObj;
                    string value = valuePattern.Current.Value;
                    textBoxFieldValue.Text = value;
                    data.Add(value);
                    return true;
                }
                object textPatternObj;
                if (targetElement.TryGetCurrentPattern(TextPattern.Pattern, out textPatternObj))
                {
                    TextPattern textPattern = (TextPattern)textPatternObj;
                    string value = textPattern.DocumentRange.GetText(-1);
                    textBoxFieldValue.Text = value;
                    data.Add(value);
                    return true;
                }
                HandleException("Element does not support ValuePattern or TextPattern");
                //MessageBox.Show("Element does not support ValuePattern or TextPattern");
                return false;
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
                // MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }

        }
        private bool passvaluefunction(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                data.Add(value);
                return true;
            }
            return false;
        }
        // Set value function
        private bool setvaluefunction(string windowXPath, string elementXPath, string value)
        {

            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {

                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    HandleException("Window not found");
                    //MessageBox.Show("Window not found");
                    return false;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    HandleException("Element not found");
                    //MessageBox.Show("Element not found");
                    return false;
                }

            }
            try
            {
                // Try ValuePattern first
                object valuePatternObj;
                if (targetElement.TryGetCurrentPattern(ValuePattern.Pattern, out valuePatternObj))
                {
                    ValuePattern valuePattern = (ValuePattern)valuePatternObj;
                    valuePattern.SetValue(value);
                    return true;
                }

                // Value pattern is not supported
                HandleException("Element does not support ValuePattern");
                //Console.WriteLine("Element does not support ValuePattern");
                return false;
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
                //Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        // Set text function
        private bool settextfunction(string windowXPath, string elementXPath, string value)
        {

            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {

                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    HandleException("Window not found");
                    //MessageBox.Show("Window not found");
                    return false;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    HandleException("Element not found");
                    //MessageBox.Show("Element not found");
                    return false;
                }

            }
            try
            {

                // try TextPattern
                object textPatternObj;
                if (targetElement.TryGetCurrentPattern(TextPattern.Pattern, out textPatternObj))
                {
                    TextPattern textPattern = (TextPattern)textPatternObj;
                    var textRange = textPattern.DocumentRange;
                    textRange.Select();
                    SendKeys.SendWait(value);
                    return true;
                }

                // Text pattern is not supported
                HandleException("Element does not support TextPattern");
                //Console.WriteLine("Element does not support TextPattern");
                return false;
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
                //Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        // Send Keys function
        private bool sendkeysfunction(string value)
        {
            try
            {
                SendKeys.SendWait(value);
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
                //MessageBox.Show("An error occurred while setting value using keyboard input: " + ex.Message);
                return false;
            }
        }
        // Send enter function
        private bool sendenterfunction()
        {
            try
            {
                SendKeys.SendWait("{ENTER}");

                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
                //Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        // Send tab function
        private bool sendtabfunction()
        {
            try
            {
                SendKeys.SendWait("{TAB}");

                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
                //Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        // Move mouse function
        private bool movemousefunction(string windowXPath, string elementXPath)
        {

            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {

                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    HandleException("Window not found");
                    //MessageBox.Show("Window not found");
                    return false;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    HandleException("Element not found");
                    //MessageBox.Show("Element not found");
                    return false;
                }

            }
            try
            {

                InteractWithElement(targetElement);
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
                //Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }

        }
        // Move mouse per pixels function
        public static bool MoveMouseAndClick(int xPixels, int yPixels)
        {
            try
            {
                var currentPos = Cursor.Position;
                int newX = currentPos.X + xPixels;
                int newY = currentPos.Y + yPixels;
                Cursor.Position = new Point(newX, newY);
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                return true;
            }
            catch (Exception ex)
            {
                //HandleException(ex.Message);
                Console.WriteLine($"Error moving mouse and clicking: {ex.Message}");
                return false;
            }
        }
        // Delay
        public bool DelayInSeconds(int milliseconds)
        {
            try
            {
                // Validate that the delay is a non-negative number
                if (milliseconds < 0)
                {
                    HandleException("Delay time cannot be negative.");
                    //MessageBox.Show("Delay time cannot be negative.");
                    return false;
                }

                // Introduce the delay
                Thread.Sleep(milliseconds);

                return true;
            }
            catch (Exception ex)
            {
                HandleException("An error occurred during the delay: " + ex.Message);
                //MessageBox.Show("An error occurred during the delay: " + ex.Message);
                return false;
            }
        }
        // Set Focus
        public bool setfocus(string windowXPath, string elementXPath)
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {

                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    HandleException("Window not found");
                    //MessageBox.Show("Window not found");
                    return false;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    HandleException("Element not found");
                    //MessageBox.Show("Element not found");
                    return false;
                }

            }
            try
            {
                targetElement.SetFocus();
                return true;
            }
            catch (Exception ex)
            {
                HandleException("An error occurred: " + ex.Message);
                //Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        // maximize window
        public bool MaximizeWindow(string elementXPath)
        {
            object patternObj;
            try
            {
                AutomationElement rootElement = AutomationElement.RootElement;
                AutomationElement windowElement = FindElementByXPath(rootElement, elementXPath);
                if (windowElement == null)
                {
                    HandleException("Window not found");
                    return false;
                }
                if (windowElement.TryGetCurrentPattern(WindowPattern.Pattern, out patternObj))
                {
                    WindowPattern windowPattern = (WindowPattern)patternObj;
                    windowPattern.SetWindowVisualState(WindowVisualState.Maximized);
                    if (windowPattern.Current.WindowVisualState == WindowVisualState.Maximized)
                    {
                        return true;
                    }
                }
                else
                {
                    HandleException("Window does not support window pattern");
                    return false;
                }
            }
            catch (Exception ex)
            {
                HandleException("An error occurred: " + ex.Message);
                return false;
            }
            return false;
        }
        // Send data to RM
        public async Task<bool> SendArrayAsync(string url, string flow, string target = null)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            if (string.IsNullOrEmpty(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                HandleException("Please enter a valid URL.");
                //MessageBox.Show("Please enter a valid URL.");
                return false;
            }
            var payload = new Dictionary<string, string>
                {
                    { "Flow", flow },
                    { "Data", jsonData }
                };
            if (!string.IsNullOrEmpty(target) && Uri.IsWellFormedUriString(target, UriKind.Absolute))
            {
                payload.Add("Target", target);
            }
            string jsonPayload = JsonConvert.SerializeObject(payload);
            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Content = content;
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        //MessageBox.Show("Data sent successfully.");
                        return true;
                    }
                    else
                    {
                        HandleException("Failed to send data. Status Code: {response.StatusCode}");
                        //MessageBox.Show($"Failed to send data. Status Code: {response.StatusCode}");
                        return false;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                HandleException($"An error occurred: {ex.Message}");
                //MessageBox.Show($"Request error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                HandleException($"An error occurred: {ex.Message}");
                //MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
        // Send Status function
        public async Task<bool> SendMessageAsync(string url, string message)
        {
            if (string.IsNullOrEmpty(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                MessageBox.Show("Please enter a valid URL.");
                return false;
            }

            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Message cannot be empty.");
                return false;
            }
            var payload = new
            {
                errorcode = 500,
                message = message,
                lastFailedAction = UIA_action
            };

            try
            {
                string jsonPayload = JsonConvert.SerializeObject(payload);
                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                // Send HTTP POST request
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Content = content;
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Failed to send message. Status Code: {response.StatusCode}");
                        return false;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}");
                return false;
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show($"Request timeout: {ex.Message}");
                return false;
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Serialization error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
                return false;
            }
        }
        // Start Debug
        public bool ToggleDebug()
        {
            UIA_debug = true;
            return true;
        }
        // Close active window
        private bool CloseWindowByXPath(string windowXPath)
        {
            object pattern;
            try
            {
                // Find the window using the provided XPath
                AutomationElement rootElement = AutomationElement.RootElement;
                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    HandleException("Window not found using the provided XPath");
                    return false;
                }

                // Check if the window supports the WindowPattern
                if (windowElement.TryGetCurrentPattern(WindowPattern.Pattern, out pattern))
                {
                    WindowPattern windowPattern = (WindowPattern)pattern;
                    windowPattern.Close();
                    return true;
                }
                else
                {
                    HandleException("The window does not support the WindowPattern, so it cannot be closed using this method");
                    return false;
                }
            }
            catch (Exception ex)
            {
                HandleException("An error occurred while trying to close the window " + ex);
                return false;
            }
        }
        // Send F5 function
        private bool sendf5function()
        {
            try
            {
                SendKeys.SendWait("{F5}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        // Get value button
        private void button3_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;
            getvaluefunction(windowXPath, elementXPath);

        }
        // Send File via http
        public static string ReadFileAsBase64(string filePath)
        {
            try
            {
                // Read the file bytes
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // Convert the byte array to a Base64 string
                string base64String = Convert.ToBase64String(fileBytes);

                return base64String;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file as Base64: {ex.Message}");
                return null;
            }
        }
        public static async Task<bool> SendBase64FileAsync(string url, string filePath)
        {
            return true;
            /*
            try
            {
                // Read the file bytes
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // Convert the byte array to a Base64 string
                string base64String = Convert.ToBase64String(fileBytes);

                // Create HTTP content with just the base64 string (no headers)
                var content = new StringContent(base64String, Encoding.UTF8, "text/plain");

                // Send the HTTP POST request
                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("File sent successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to send file. Status code: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
            */
        }
        // Empty button
        private void button4_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;
            int timeoutSeconds;
            // Get the timeout value from the TextBox
            if (!int.TryParse(textBoxTimeout.Text, out timeoutSeconds))
            {
                // Handle invalid timeout input
                MessageBox.Show("Invalid timeout value");
                return;
            }
            // wait element
            WaitForElementByXPath(windowXPath, elementXPath, timeoutSeconds);
        }
        // Send http response
        public static async Task SendSuccessResponse(HttpListenerContext context, int actionCount)
        {
            var successResponse = new
            {
                message = "Request processed successfully",
                actionCount = actionCount,
                timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
            };
            string successResponseString = JsonConvert.SerializeObject(successResponse);
            byte[] buffer = Encoding.UTF8.GetBytes(successResponseString);
            HttpListenerResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentLength64 = buffer.Length;
            using (System.IO.Stream output = response.OutputStream)
            {
                await output.WriteAsync(buffer, 0, buffer.Length);
            }
        }
        // Copy File Function
        public bool CopyAndRenameFile(string path1, string path2, string newFileName)
        {
            try
            {
                if (string.IsNullOrEmpty(path1) || !File.Exists(path1))
                {
                    HandleException("Original path is invalid or file does not exist.");
                    //MessageBox.Show("Original path is invalid or file does not exist.");
                    return false;
                }
                if (string.IsNullOrEmpty(path2) || !Directory.Exists(path2))
                {
                    HandleException("Destination path is invalid or inaccessible.");
                    //MessageBox.Show("Destination path is invalid or inaccessible.");
                    return false;
                }
                if (string.IsNullOrEmpty(newFileName))
                {
                    HandleException("New file name cannot be empty.");
                    //MessageBox.Show("New file name cannot be empty.");
                    return false;
                }
                string destinationFilePath = Path.Combine(path2, newFileName);
                File.Copy(path1, destinationFilePath, overwrite: true);
                return true;
            }
            catch (Exception ex)
            {
                HandleException($"An error occurred: {ex.Message}");
                //MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
        // Move File Function
        public bool MoveAndRenameFile(string path1, string path2, string newFileName)
        {
            try
            {
                if (string.IsNullOrEmpty(path1) || !File.Exists(path1))
                {
                    HandleException("Original path is invalid or file does not exist.");
                    return false;
                }
                if (string.IsNullOrEmpty(path2) || !Directory.Exists(path2))
                {
                    HandleException("Destination path is invalid or inaccessible.");
                    return false;
                }
                if (string.IsNullOrEmpty(newFileName))
                {
                    HandleException("New file name cannot be empty.");
                    return false;
                }

                string destinationFilePath = Path.Combine(path2, newFileName);

                // Move the file instead of copying it
                File.Move(path1, destinationFilePath);

                return true;
            }
            catch (Exception ex)
            {
                HandleException($"An error occurred: {ex.Message}");
                return false;
            }
        }
        // Kill Task
        public bool KillTask(string processName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    HandleException($"No processes found with the name: {processName}");
                    return false;
                }
                foreach (Process process in processes)
                {
                    process.Kill();
                    process.WaitForExit();
                    //Console.WriteLine($"Process {process.ProcessName} (ID: {process.Id}) has been killed.");
                }

                return true;
            }
            catch (Exception ex)
            {
                HandleException($"An error occurred while trying to kill the process: {ex.Message}");
                return false;
            }
        }
        // Kill Task by Powershell
        public bool KillProcess(string processName)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("taskkill", $"/F /IM \"{processName}.exe\"")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrEmpty(error))
                    {
                        HandleException($"Error killing process: {error}");
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                HandleException($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public bool StartTask(string executablePath, string arguments = "")
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = executablePath,
                    Arguments = arguments
                };
                Process.Start(startInfo);
                //Console.WriteLine($"Task started: {executablePath} with arguments: {arguments}");
                return true;
            }
            catch (Exception ex)
            {
                HandleException($"An error occurred while trying to start the process: {ex.Message}");
                return false;
            }
        }
        // http request logger 
        private void LogRequestToFile(string requestBody)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "UIA Logs");

                // Create the folder if it doesn't exist
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Define the file path
                string filePath = Path.Combine(folderPath, "HttpLog.json");

                // Write the request body to the file
                File.WriteAllText(filePath, requestBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging request: {ex.Message}");
            }
        }
        // Http server listner
        private void StopListener()
        {
            if (_listener != null && _isListenerRunning)
            {
                _listener.Stop();
                _listener = null;
                _isListenerRunning = false;

            }
        }
        private async Task ListenForRequests()
        {

            while (_isListenerRunning)
            {
                try
                {
                    HttpListenerContext context = await _listener.GetContextAsync();
                    HttpListenerRequest request = context.Request;
                    if (request.RawUrl.Contains("/ping") || request.QueryString["ping"] == "1")
                    {
                        var heartbeatResponse = new
                        {
                            code = 200,
                            Status = "Heartbeast received: RDP SSH Connection is alive",
                            timestamp = DateTime.UtcNow.ToString("HH:mm:ss yyyy-MM-dd")
                        };
                        string heartbeatResponseString = JsonConvert.SerializeObject(heartbeatResponse);
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(heartbeatResponseString);

                        HttpListenerResponse response = context.Response;
                        response.ContentType = "application/json";
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ContentLength64 = buffer.Length;
                        using (System.IO.Stream output = response.OutputStream)
                        {
                            await output.WriteAsync(buffer, 0, buffer.Length);
                        }
                        continue;
                    }
                    string rpaParam = request.QueryString["rpa"];
                    if (string.IsNullOrEmpty(rpaParam) || rpaParam != "1")
                    {
                        var errorResponse = new
                        {
                            error = new
                            {
                                code = 400,
                                message = "Invalid or missing 'rpa' query parameter",
                                details = "Ensure that the 'rpa' query parameter is included in the request and its value is set to '1'."
                            },
                            timestamp = DateTime.UtcNow.ToString("HH:mm:ss yyyy-MM-dd")
                        };
                        string errorResponseString = JsonConvert.SerializeObject(errorResponse);
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(errorResponseString);

                        HttpListenerResponse response = context.Response;
                        response.ContentType = "application/json";
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.ContentLength64 = buffer.Length;
                        using (System.IO.Stream output = response.OutputStream)
                        {
                            await output.WriteAsync(buffer, 0, buffer.Length);
                        }
                        continue;
                    }

                    string requestBody;
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        requestBody = await reader.ReadToEndAsync();
                    }
                    LogRequestToFile(requestBody);
                    JArray jsonArray = null;
                    try
                    {
                        jsonArray = JArray.Parse(requestBody);
                    }
                    catch (JsonReaderException jex)
                    {
                        var errorResponse = new
                        {
                            error = new
                            {
                                code = 400,
                                message = "JSON parsing error",
                                details = jex.Message
                            },
                            timestamp = DateTime.UtcNow.ToString("HH:mm:ss yyyy-MM-dd")
                        };

                        string errorResponseString = JsonConvert.SerializeObject(errorResponse);
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(errorResponseString);

                        HttpListenerResponse response = context.Response;
                        response.ContentType = "application/json";
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.ContentLength64 = buffer.Length;
                        using (System.IO.Stream output = response.OutputStream)
                        {
                            await output.WriteAsync(buffer, 0, buffer.Length);
                        }
                        continue;
                    }

                    if (jsonArray == null)
                    {
                        var errorResponse = new
                        {
                            error = new
                            {
                                code = 400,
                                message = "No valid JSON found in the request.",
                                details = "The JSON body could not be parsed."
                            },
                            timestamp = DateTime.UtcNow.ToString("HH:mm:ss yyyy-MM-dd")
                        };

                        string errorResponseString = JsonConvert.SerializeObject(errorResponse);
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(errorResponseString);

                        HttpListenerResponse response = context.Response;
                        response.ContentType = "application/json";
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.ContentLength64 = buffer.Length;
                        using (System.IO.Stream output = response.OutputStream)
                        {
                            await output.WriteAsync(buffer, 0, buffer.Length);
                        }
                        continue;
                    }

                    List<Dictionary<string, string>> actions = jsonArray
                        .Select(action => action.ToObject<Dictionary<string, string>>())
                        .ToList();

                    if (actions != null && actions.Count > 0)
                    {

                        bool actionsSuccessful = await StartActions(actions);
                        if (actionsSuccessful)
                        {
                            var successResponse = new
                            {
                                message = "Request processed successfully",
                                actionCount = actions.Count,
                                timestamp = DateTime.UtcNow.ToString("HH:mm:ss yyyy-MM-dd"),
                                getData = data
                            };
                            string successResponseString = JsonConvert.SerializeObject(successResponse);
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(successResponseString);

                            HttpListenerResponse response = context.Response;
                            response.ContentType = "application/json";
                            response.StatusCode = (int)HttpStatusCode.OK;
                            response.ContentLength64 = buffer.Length;

                            using (System.IO.Stream output = response.OutputStream)
                            {
                                await output.WriteAsync(buffer, 0, buffer.Length);
                            }
                        }
                        else
                        {
                            var errorResponse = new
                            {
                                message = "The UIA automation was executed but did not finish properly, please check your instructions and reset your environment."
                            };
                            string errorResponseString = JsonConvert.SerializeObject(errorResponse);
                            byte[] errorBuffer = System.Text.Encoding.UTF8.GetBytes(errorResponseString);
                            HttpListenerResponse response = context.Response;
                            response.ContentType = "application/json";
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            response.ContentLength64 = errorBuffer.Length;
                            using (System.IO.Stream output = response.OutputStream)
                            {
                                await output.WriteAsync(errorBuffer, 0, errorBuffer.Length);
                            }
                        }

                    }

                }
                catch (HttpListenerException ex)
                {
                    MessageBox.Show($"HttpListenerException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception: {ex.Message}");
                }
            }

        }
        private void StartListener(string prefix)
        {
            if (_listener != null && _isListenerRunning)
            {
                StopListener(); // Stop the current listener if it's running
            }

            _listener = new HttpListener();
            _prefix = prefix;

            try
            {
                _listener.Prefixes.Add(_prefix);
                _listener.Start();
                _isListenerRunning = true;

                // Notify the user that the server has started
                MessageBox.Show("Server started successfully at " + _prefix, "Server Started", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Run your listening logic on a separate task
                Task.Run(() => ListenForRequests());
            }
            catch (HttpListenerException ex)
            {
                HandleHttpListenerException(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting listener: {ex.Message}", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isListenerRunning = false;
            }
        }
        private void HandleHttpListenerException(HttpListenerException ex)
        {
            switch (ex.ErrorCode)
            {
                case 5: // Access Denied
                    MessageBox.Show("Access Denied. Please run as administrator or configure URL ACLs.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 183: // URL already reserved
                    MessageBox.Show("The specified URL is already reserved. Try using a different port or URL.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 87: // Invalid Parameter
                    MessageBox.Show("Invalid parameter. Check the URL format: " + _prefix, "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show($"HttpListener Error: {ex.Message} (Code: {ex.ErrorCode})", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            _isListenerRunning = false;
        }
        // tcp server listner
        private void StartTcpServer(string prefix)
        {
            int port;
            string[] parts = prefix.Split(':');

            if (parts.Length == 2 && int.TryParse(parts[1], out port))
            {
                try
                {
                    _tcpListener = new TcpListener(IPAddress.Any, port);
                    _tcpListener.Start();
                    _istcpListenerRunning = true;

                    // Notify the user that the TCP server has started
                    MessageBox.Show("TCP Server started successfully at " + prefix, "Server Started", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Run the TCP listening logic in a separate task
                    Task.Run(() => ListenForTcpConnections());
                }
                catch (SocketException ex)
                {
                    // Log specific SocketException errors
                    MessageBox.Show($"Socket error while starting TCP server: {ex.Message} (Error Code: {ex.ErrorCode})", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _isListenerRunning = false;
                }
                catch (Exception ex)
                {
                    // Log general exceptions
                    MessageBox.Show($"Error while starting TCP server: {ex.Message}", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _isListenerRunning = false;
                }
            }
            else
            {
                MessageBox.Show("Invalid TCP prefix format. Example format: 127.0.0.1:4000", "Invalid Prefix", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // stop server
        private void StopTcpServer()
        {
            if (_tcpListener != null && _isListenerRunning)
            {
                try
                {
                    _tcpListener.Stop();
                    _isListenerRunning = false;

                    // Notify the user that the TCP server has stopped
                    MessageBox.Show("TCP Server stopped successfully.", "Server Stopped", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur while stopping the server
                    MessageBox.Show($"Error while stopping TCP server: {ex.Message}", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _tcpListener = null;
                }
            }
            else
            {
                // Log a message if the server is not running or already stopped
                MessageBox.Show("TCP Server is not running or has already been stopped.", "Server Not Running", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ListenForTcpConnections()
        {
            try
            {
                while (_isListenerRunning)
                {
                    // Accept a pending client connection
                    TcpClient client = _tcpListener.AcceptTcpClient();

                    // Process the client connection in a separate task
                    Task.Run(() => ProcessClient(client));
                }
            }
            catch (Exception ex)
            {
                // Log errors related to accepting connections
                MessageBox.Show($"Error while listening for TCP connections: {ex.Message}", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ProcessClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                // Read incoming data
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Echo the data back to the client (for testing purposes)
                byte[] responseData = Encoding.ASCII.GetBytes("Received: " + dataReceived);
                stream.Write(responseData, 0, responseData.Length);

                // Close the client connection
                client.Close();
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing the client
                MessageBox.Show($"Error while processing client: {ex.Message}", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Execute actions button
        private async void button10_Click(object sender, EventArgs e)
        {
            // reload Actions file
            LoadActionsFromFile();
            // Clear the data list to reset it
            data.Clear();
            // start sequance of actions
            foreach (var action in actions)
            {
                bool breakValue;
                string description = action.ContainsKey("Description") ? (string)action["Description"] : "No description";
                UIA_action = description;
                string actionType = action["actionType"];
                string url = action.ContainsKey("url") ? action["url"] : null;
                string flow = action.ContainsKey("flow") ? action["flow"] : null;
                string target = action.ContainsKey("target") ? action["target"] : null;
                string windowXPath = action.ContainsKey("windowXPath") ? action["windowXPath"] : null;
                string elementXPath = action.ContainsKey("elementXPath") ? action["elementXPath"] : null;
                string value = action.ContainsKey("value") ? action["value"] : null;
                string file = action.ContainsKey("file") ? action["file"] : null;
                bool debug = action.ContainsKey("debug") && bool.TryParse(action["debug"], out UIA_debug) ? UIA_debug : false;
                string debugUrl = action.ContainsKey("debugUrl") ? action["debugUrl"] : null;
                bool breakFlag = action.ContainsKey("break") && bool.TryParse(action["break"], out breakValue) ? breakValue : true;
                int timeout;
                int timer;
                int x = 0;
                int y = 0;
                string path1 = action.ContainsKey("path1") ? action["path1"] : null;
                string path2 = action.ContainsKey("path2") ? action["path2"] : null;
                string FileName = action.ContainsKey("filename") ? action["filename"] : null;
                string exe = action.ContainsKey("exe") ? action["exe"] : null;
                string arg = action.ContainsKey("arg") ? action["arg"] : null;
                string process = action.ContainsKey("process") ? action["process"] : null;
                if (action.ContainsKey("timeout"))
                {
                    bool isValidTimeout = int.TryParse(action["timeout"], out timeout);
                    if (!isValidTimeout)
                    {
                        timeout = 4;
                    }
                }
                else
                {
                    timeout = 4;
                }
                if (action.ContainsKey("timer"))

                {
                    bool isValidTimeout = int.TryParse(action["timer"], out timer);
                    if (!isValidTimeout)
                    {
                        timer = 1000;
                    }
                }
                else
                {
                    timer = 1000;
                }
                if (action.ContainsKey("x"))
                {
                    bool isValidX = int.TryParse(action["x"], out x);
                    if (!isValidX)
                    {
                        x = 10;
                    }
                }
                else
                {
                    x = 10;
                }
                if (action.ContainsKey("y"))
                {
                    bool isValidY = int.TryParse(action["y"], out y);
                    if (!isValidY)
                    {
                        y = 10;
                    }
                }
                else
                {
                    y = 10;
                }
                bool isSuccess = false;

                if (actionType == "Click")
                {
                    isSuccess = ClickElementByXPath(windowXPath, elementXPath);
                }
                else if (actionType == "SetValue")
                {
                    isSuccess = setvaluefunction(windowXPath, elementXPath, value);
                }
                else if (actionType == "CopyFile")
                {
                    isSuccess = CopyAndRenameFile(path1, path2, FileName);
                }
                else if (actionType == "MoveFile")
                {
                    isSuccess = MoveAndRenameFile(path1, path2, FileName);
                }
                else if (actionType == "SetText")
                {
                    isSuccess = settextfunction(windowXPath, elementXPath, value);
                }
                else if (actionType == "SendKeys")
                {
                    isSuccess = sendkeysfunction(value);
                }
                else if (actionType == "Enter")
                {
                    isSuccess = sendenterfunction();
                }
                else if (actionType == "Tab")
                {
                    isSuccess = sendtabfunction();
                }
                else if (actionType == "Get")
                {
                    isSuccess = getvaluefunction(windowXPath, elementXPath);
                }
                else if (actionType == "Pass")
                {
                    isSuccess = passvaluefunction(value);
                }
                else if (actionType == "Wait")
                {
                    isSuccess = WaitForElementByXPath(windowXPath, elementXPath, timeout);
                }
                else if (actionType == "MoveMouse")
                {
                    isSuccess = movemousefunction(windowXPath, elementXPath);
                }
                else if (actionType == "Sleep")
                {
                    isSuccess = DelayInSeconds(timer);
                }
                else if (actionType == "MaximizeWindow")
                {
                    isSuccess = MaximizeWindow(elementXPath);
                }
                else if (actionType == "Focus")
                {
                    isSuccess = setfocus(windowXPath, elementXPath);
                }
                else if (actionType == "Focus")
                {
                    isSuccess = setfocus(windowXPath, elementXPath);
                }
                else if (actionType == "Send")
                {
                    isSuccess = true;
                    await SendArrayAsync(url, flow, target);
                }
                else if (actionType == "F5")
                {
                    isSuccess = sendf5function();
                }
                else if (actionType == "CloseWindow")
                {
                    isSuccess = CloseWindowByXPath(elementXPath);
                }
                else if (actionType == "KillTask")
                {
                    isSuccess = KillTask(process);
                }
                else if (actionType == "KillProcess")
                {
                    isSuccess = KillProcess(process);
                }
                else if (actionType == "StartTask")
                {
                    isSuccess = StartTask(exe, arg);
                }
                else if (actionType == "file")
                {
                    isSuccess = await SendBase64FileAsync(url, file);
                }
                else if (actionType == "mouse")
                {
                    isSuccess = MoveMouseAndClick(x, y);
                }
                else
                {
                    Console.WriteLine("Unknown action type: " + actionType);
                }
                /*if (!isSuccess)
                {
                    // MessageBox.Show($"Action failed at: {description}", "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HandleException(($"Action failed at: {description}"));
                    break;
                }
                */
                if (!isSuccess)
                {
                    if (breakFlag)
                    {
                        HandleException(($"Action failed at: {description}"));
                        break;
                    }
                }
            }
        }
        // Start actions function
        private async Task<bool> StartActions(List<Dictionary<string, string>> actions)
        {
            // Clear the data list to reset it
            data.Clear();
            // start sequance of actions
            foreach (var action in actions)
            {
                bool breakValue;
                string description = action.ContainsKey("Description") ? action["Description"] : "No description";
                UIA_action = description;
                string actionType = action["actionType"];
                string url = action.ContainsKey("url") ? action["url"] : null;
                string flow = action.ContainsKey("flow") ? action["flow"] : null;
                string target = action.ContainsKey("target") ? action["target"] : null;
                string windowXPath = action.ContainsKey("windowXPath") ? action["windowXPath"] : null;
                string elementXPath = action.ContainsKey("elementXPath") ? action["elementXPath"] : null;
                bool breakFlag = action.ContainsKey("break") && bool.TryParse(action["break"], out breakValue) ? breakValue : true;
                string value = action.ContainsKey("value") ? action["value"] : null;
                string file = action.ContainsKey("file") ? action["file"] : null;
                int timeout;
                int timer;
                int x = 0;
                int y = 0;
                string path1 = action.ContainsKey("path1") ? action["path1"] : null;
                string path2 = action.ContainsKey("path2") ? action["path2"] : null;
                string FileName = action.ContainsKey("filename") ? action["filename"] : null;
                string exe = action.ContainsKey("exe") ? action["exe"] : null;
                string arg = action.ContainsKey("arg") ? action["arg"] : null;
                string process = action.ContainsKey("process") ? action["process"] : null;
                if (action.ContainsKey("timeout"))
                {
                    bool isValidTimeout = int.TryParse(action["timeout"], out timeout);
                    if (!isValidTimeout)
                    {
                        timeout = 4;
                    }
                }
                else
                {
                    timeout = 4;
                }
                if (action.ContainsKey("timer"))

                {
                    bool isValidTimeout = int.TryParse(action["timer"], out timer);
                    if (!isValidTimeout)
                    {
                        timer = 1000;
                    }
                }
                else
                {
                    timer = 1000;
                }
                if (action.ContainsKey("x"))
                {
                    bool isValidx = int.TryParse(action["x"], out x);
                    if (!isValidx)
                    {
                        x = 10;
                    }
                }
                else
                {
                    x = 10;
                }
                if (action.ContainsKey("y"))
                {
                    bool isValidy = int.TryParse(action["y"], out x);
                    if (!isValidy)
                    {
                        y = 10;
                    }
                }
                else
                {
                    y = 10;
                }
                bool isSuccess = false;

                if (actionType == "Click")
                {
                    isSuccess = ClickElementByXPath(windowXPath, elementXPath);
                }
                else if (actionType == "SetValue")
                {
                    isSuccess = setvaluefunction(windowXPath, elementXPath, value);
                }
                else if (actionType == "CopyFile")
                {
                    isSuccess = CopyAndRenameFile(path1, path2, FileName);
                }
                else if (actionType == "MoveFile")
                {
                    isSuccess = MoveAndRenameFile(path1, path2, FileName);
                }
                else if (actionType == "SetText")
                {
                    isSuccess = settextfunction(windowXPath, elementXPath, value);
                }
                else if (actionType == "SendKeys")
                {
                    isSuccess = sendkeysfunction(value);
                }
                else if (actionType == "Enter")
                {
                    isSuccess = sendenterfunction();
                }
                else if (actionType == "Enter")
                {
                    isSuccess = sendtabfunction();
                }
                else if (actionType == "Get")
                {
                    isSuccess = getvaluefunction(windowXPath, elementXPath);
                }
                else if (actionType == "MaximizeWindow")
                {
                    isSuccess = MaximizeWindow(elementXPath);
                }
                else if (actionType == "Pass")
                {
                    isSuccess = passvaluefunction(value);
                }
                else if (actionType == "Wait")
                {
                    isSuccess = WaitForElementByXPath(windowXPath, elementXPath, timeout);
                }
                else if (actionType == "MoveMouse")
                {
                    isSuccess = movemousefunction(windowXPath, elementXPath);
                }
                else if (actionType == "Sleep")
                {
                    isSuccess = DelayInSeconds(timer);
                }
                else if (actionType == "Focus")
                {
                    isSuccess = setfocus(windowXPath, elementXPath);
                }
                else if (actionType == "Focus")
                {
                    isSuccess = setfocus(windowXPath, elementXPath);
                }
                else if (actionType == "Send")
                {
                    isSuccess = true;
                    await SendArrayAsync(url, flow, target);
                }
                else if (actionType == "F5")
                {
                    isSuccess = sendf5function();
                }
                else if (actionType == "CloseWindow")
                {
                    isSuccess = CloseWindowByXPath(elementXPath);
                }
                else if (actionType == "file")
                {
                    isSuccess = await SendBase64FileAsync(url, file);
                }
                else if (actionType == "mouse")
                {
                    isSuccess = MoveMouseAndClick(x, y);
                }
                else if (actionType == "KillTask")
                {
                    isSuccess = KillTask(process);
                }
                else if (actionType == "KillProcess")
                {
                    isSuccess = KillProcess(process);
                }
                else if (actionType == "StartTask")
                {
                    isSuccess = StartTask(exe, arg);
                }
                else
                {
                    Console.WriteLine("Unknown action type: " + actionType);
                }
                /*if (!isSuccess)
                {
                    // MessageBox.Show($"Action failed at: {description}", "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HandleException(($"Action failed at: {description}"));
                    break;
                }
                */
                if (!isSuccess)
                {
                    if (breakFlag)
                    {
                        HandleException(($"Action failed at: {description}"));
                        return false;
                    }
                }
            }
            if (actions != null && actions.Count > 0)
            {
                int actionCount = actions.Count;
            }
            return true;
        }
        // Send Keys
        private void button5_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;
            string newValue = textBoxNewValue.Text;

            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {

                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    MessageBox.Show("Window not found");
                    return;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    MessageBox.Show("Element not found");
                    return;
                }

            }

            try
            {
                // Set focus on the target element
                //targetElement.SetFocus();

                // Clear any existing text
                //SendKeys.SendWait("^{HOME}"); // Move to the start
                //SendKeys.SendWait("^+{END}"); // Select all
                //SendKeys.SendWait("{DEL}");   // Delete

                // Send the value character by character
                SendKeys.SendWait(newValue);

                // Optionally, press Enter to confirm the input
                SendKeys.SendWait("{ENTER}");

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while setting value using keyboard input: " + ex.Message);
                return;
            }

        }
        // Hover mouse
        public bool InteractWithElement(AutomationElement targetElement)
        {
            try
            {
                // Convert the BoundingRectangle from System.Windows.Rect to System.Drawing.Rectangle
                System.Windows.Rect boundingRect = targetElement.Current.BoundingRectangle;
                Rectangle drawingRect = new Rectangle(
                    (int)boundingRect.X,
                    (int)boundingRect.Y,
                    (int)boundingRect.Width,
                    (int)boundingRect.Height
                );

                // Click in the middle of the element's bounding rectangle
                MouseClick(drawingRect.X + drawingRect.Width / 2, drawingRect.Y + drawingRect.Height / 4);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while interacting with the element: " + ex.Message);
                return false;
            }
        }
        // click mouse
        private new void MouseClick(int x, int y)
        {
            // Move the mouse to the specified coordinates
            SetCursorPos(x, y);
            //Cursor.Position = new Point(x, y);

            // Simulate a left mouse button down and up to click
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);
        }
        // Move mouse button
        private void button7_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;
            string newValue = textBoxNewValue.Text;

            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {

                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    MessageBox.Show("Window not found");
                    return;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    MessageBox.Show("Element not found");
                    return;
                }

            }
            try
            {

                InteractWithElement(targetElement);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return;
            }

        }
        // Legacy set value pattern
        private void button8_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;
            string newValue = textBoxNewValue.Text;

            AutomationElement rootElement = AutomationElement.RootElement;
            AutomationElement targetElement = null;

            if (string.IsNullOrEmpty(windowXPath))
            {
                // Search the elementXPath directly within the root element
                targetElement = FindElementByXPath(rootElement, elementXPath);
            }
            else
            {
                AutomationElement windowElement = FindElementByXPath(rootElement, windowXPath);

                if (windowElement == null)
                {
                    MessageBox.Show("Window not found");
                    return;
                }

                targetElement = FindElementByXPath(windowElement, elementXPath);
                if (targetElement == null)
                {
                    MessageBox.Show("Element not found");
                    return;
                }
            }
            try
            {
                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return;
            }

        }
        private void button9_Click(object sender, EventArgs e)
        {
            string windowXPath = txtWindowXPath.Text;
            string elementXPath = txtElementXPath.Text;
            //
            setfocus(windowXPath, elementXPath);
        }
        //Start listner button
        private void button11_Click(object sender, EventArgs e)
        {
            string newPrefix = textBoxPrefix.Text;

            if (_isListenerRunning)
            {
                StopListener();
            }

            if (!string.IsNullOrEmpty(newPrefix))
            {
                StartListener(newPrefix);
            }
            else
            {
                MessageBox.Show("Prefix cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Start tcp Listner button
        private void button12_Click(object sender, EventArgs e)
        {
            string newPrefix = textBoxTcpPrefix.Text;

            if (_isListenerRunning)
            {
                StopListener();
            }

            if (!string.IsNullOrEmpty(newPrefix))
            {
                StartTcpServer(newPrefix);
            }
            else
            {
                MessageBox.Show("Prefix cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (_isListenerRunning)
            {
                StopListener();
            }
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            StopTcpServer();
        }
        private void button9_Click_1(object sender, EventArgs e)
        {
            // Read the input from the TextBox in the format "server:port"
            string input = textBoxTcpPrefix.Text;
            string[] parts = input.Split(':');

            if (parts.Length != 2)
            {
                MessageBox.Show("Invalid input format. Use 'server:port'.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int port;
            string server = parts[0];
            if (!int.TryParse(parts[1], out port))
            {
                MessageBox.Show("Invalid port number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            try
            {
                // Create a TcpClient.
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a byte array.
                string message = "Hello, server!";
                byte[] data = Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);
                MessageBox.Show("Sent: " + message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Receive the TcpServer response.
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                string responseData = Encoding.ASCII.GetString(data, 0, bytes);
                MessageBox.Show("Received: " + responseData, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("ArgumentNullException: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Close Window 
        private void button13_Click(object sender, EventArgs e)
        {

            string elementXPath = txtElementXPath.Text;

            CloseWindowByXPath(elementXPath);
        }
        // Notify Icon
        private void SetupNotificationIcon()
        {
            notifyIcon1.Text = "Oakland ROPA [PHME]";
            // tools tip menus
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem appNameMenuItem = new ToolStripMenuItem("Oakland RPA - [PHME]");
            appNameMenuItem.Enabled = false;
            //appNameMenuItem.Image = new Icon("notifyIcon1.Icon").ToBitmap();
            ToolStripMenuItem startActionsItem = new ToolStripMenuItem("Start Actions", null, button10_Click);
            ToolStripMenuItem httpListenerItem = new ToolStripMenuItem("Start HTTP Listener", null, HttpListener_Click);
            ToolStripMenuItem tcpListenerItem = new ToolStripMenuItem("Start TCP Listener", null, TcpListener_Click);
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit", null, Exit_Click);
            ToolStripSeparator separator = new ToolStripSeparator();
            // context menu
            contextMenu.Items.Add(appNameMenuItem);
            contextMenu.Items.Add(separator);
            contextMenu.Items.Add(startActionsItem);
            contextMenu.Items.Add(httpListenerItem);
            contextMenu.Items.Add(tcpListenerItem);
            contextMenu.Items.Add(exitItem);

            // nontif
            notifyIcon1.ContextMenuStrip = contextMenu;
            notifyIcon1.Visible = true;
            notifyIcon1.DoubleClick += NotifyIcon_DoubleClick;
        }
        private void HttpListener_Click(object sender, EventArgs e)
        {
            string newPrefix = textBoxPrefix.Text;
            if (_isListenerRunning)
            {
                StopListener();
                _isListenerRunning = false;
                contextMenu.Items[3].Text = "Start HTTP Listener";
            }
            else
            {
                StartListener(newPrefix);
                _isListenerRunning = true;
                contextMenu.Items[3].Text = "Stop HTTP Listener";
            }
        }
        private void TcpListener_Click(object sender, EventArgs e)
        {
            string newPrefix = textBoxTcpPrefix.Text;
            if (_istcpListenerRunning)
            {
                StopTcpServer();
                _istcpListenerRunning = false;
                contextMenu.Items[4].Text = "Start TCP Listener";  // Update menu item text
            }
            else
            {
                StartTcpServer(newPrefix);
                _istcpListenerRunning = true;
                contextMenu.Items[4].Text = "Stop TCP Listener";
            }
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Exit the application
        }
        // minimize notif
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (WindowState == FormWindowState.Minimized)
            {
                Hide();  // Hide the form
                notifyIcon1.Visible = true;
            }
        }
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();  // Show the form
            WindowState = FormWindowState.Normal;  // Restore the window state
            notifyIcon1.Visible = false;  // Hide the notification icon
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        // Picture debug toggler
        private void LoadFile_Click(object sender, EventArgs e)
        {
            // Code for "Load" functionality
        }
        private void About_Click(object sender, EventArgs e)
        {
            // Create and show the About form
            about aboutForm = new about();
            aboutForm.ShowDialog();
        }
        private void terns_Click(object sender, EventArgs e)
        {
            // Create and show the About form
            terms termsForm = new terms();
            termsForm.ShowDialog();
        }
        private void startsshbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sshportTextBox.Text))
            {
                MessageBox.Show("Please enter a port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string port = sshportTextBox.Text;
            string targetPath = @"C:\Program Files\PuTTY";
            string cmd = $"cd \"{targetPath}\" && plink -R 80:localhost:{port} serveo.net";

            // pre config
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C " + cmd;

            // config
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = false;

            //start
            process.Start();
        }
        // Register Debug URL 
        private void button14_Click(object sender, EventArgs e)
        {
            string inputText = textBoxRMUrl.Text.Trim();
            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("Error: URL cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidUrl(inputText))
            {
                MessageBox.Show("Error: Invalid URL format!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UIA_debugUrl = textBoxRMUrl.Text;
            MessageBox.Show("URL accepted and processed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool IsValidUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                          (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void closeApp_Click(object sender, EventArgs e)
        {
            // Close the application window
            this.Close();
        }

        private void minApp_Click(object sender, EventArgs e)
        {
            // Minimize the application window
            this.WindowState = FormWindowState.Minimized;
        }

        private void dockApp_Click(object sender, EventArgs e)
        {
            // Toggle between maximized and normal window state
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }

}
