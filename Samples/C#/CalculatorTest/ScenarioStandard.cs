//******************************************************************************
//
// Copyright (c) 2017 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CalculatorTest
{
    [TestClass]
    public class ScenarioStandard : CalculatorSession
    {
        private static WindowsElement header;
        private static WindowsElement calculatorResult;
        private const string Input = @"G:\My Drive\SanJose_Team\PNTDATA\BORROWER";
        private const string Output = @"C:\Users\Agent\Downloads\fnm\";

        [TestMethod]
        public void ExportFromPoint()
        {
            Console.WriteLine("Exporting files...");

            string[] fileEntries = Directory.GetFiles(Input, "*brw");
            foreach (string file in fileEntries)
            {
                Console.WriteLine(file);
                string fnmFile = Output + Path.GetFileName(file) + ".fnm";
                if (!File.Exists(fnmFile))
                {
                session.FindElementByName("File").Click();
                session.FindElementByName("Open File...").Click();
                Thread.Sleep(TimeSpan.FromSeconds(2));
                //session.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                session.Keyboard.SendKeys(file + Keys.Enter);
                Thread.Sleep(TimeSpan.FromSeconds(2));
                session.FindElementByName("File").Click();
                session.FindElementByName("Export To").Click();
                session.FindElementByName("Fannie Mae 3.2 DO/DU (Local)...").Click();
                session.Keyboard.SendKeys(fnmFile + Keys.Enter);
                Thread.Sleep(TimeSpan.FromSeconds(1));
                session.Keyboard.SendKeys(Keys.Enter);
                }
            }
        }

        [TestMethod]
        public void Addition()
        {
            // Find the buttons by their names and click them in sequence to peform 1 + 7 = 8
            session.FindElementByName("One").Click();
            session.FindElementByName("Plus").Click();
            session.FindElementByName("Seven").Click();
            session.FindElementByName("Equals").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestMethod]
        public void Division()
        {
            // Find the buttons by their accessibility ids and click them in sequence to perform 88 / 11 = 8
            session.FindElementByAccessibilityId("num8Button").Click();
            session.FindElementByAccessibilityId("num8Button").Click();
            session.FindElementByAccessibilityId("divideButton").Click();
            session.FindElementByAccessibilityId("num1Button").Click();
            session.FindElementByAccessibilityId("num1Button").Click();
            session.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestMethod]
        public void Multiplication()
        {
            // Find the buttons by their names using XPath and click them in sequence to perform 9 x 9 = 81
            session.FindElementByXPath("//Button[@Name='Nine']").Click();
            session.FindElementByXPath("//Button[@Name='Multiply by']").Click();
            session.FindElementByXPath("//Button[@Name='Nine']").Click();
            session.FindElementByXPath("//Button[@Name='Equals']").Click();
            Assert.AreEqual("81", GetCalculatorResultText());
        }

        [TestMethod]
        public void Subtraction()
        {
            // Find the buttons by their accessibility ids using XPath and click them in sequence to perform 9 - 1 = 8
            session.FindElementByXPath("//Button[@AutomationId=\"num9Button\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"minusButton\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"num1Button\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"equalButton\"]").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestMethod]
        [DataRow("One",   "Plus",      "Seven", "8")]
        [DataRow("Nine",  "Minus",     "One",   "8")]
        [DataRow("Eight", "Divide by", "Eight", "1")]
        public void Templatized(string input1, string operation, string input2, string expectedResult)
        {
            // Run sequence of button presses specified above and validate the results
            session.FindElementByName(input1).Click();
            session.FindElementByName(operation).Click();
            session.FindElementByName(input2).Click();
            session.FindElementByName("Equals").Click();
            Assert.AreEqual(expectedResult, GetCalculatorResultText());
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch a Calculator window
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        private string GetCalculatorResultText()
        {
            return calculatorResult.Text.Replace("Display is", string.Empty).Trim();
        }
    }
}
