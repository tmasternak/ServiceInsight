﻿namespace Particular.ServiceInsight.FunctionalTests.UI.Parts
{
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation;
    using Shouldly;
    using TestStack.White.UIItems;
    using TestStack.White.UIItems.Finders;
    using TestStack.White.UIItems.WindowItems;
    using TestStack.White.UIItems.WPFUIItems;

    public class LayoutManager : UIElement
    {
        GroupBox barManager;
        IUIItem[] autoHideGroups;

        public LayoutManager(Window mainWindow) : base(mainWindow)
        {
            barManager = mainWindow.Get<GroupBox>("BarManager");
            autoHideGroups = barManager.GetMultiple(SearchCriteria.ByClassName("AutoHideGroup"));
        }

        public void DockAutoHideGroups()
        {
            foreach (var item in autoHideGroups)
            {
                Dock(item);
                Thread.Sleep(1000);
            }
        }

        void Dock(IUIItem item)
        {
            //TODO: Workaround. Can not find dockmanager's context menu. Try to remove as it should have been fixed now.
            item.RightClick();
            Mouse.Location = new Point(Mouse.Location.X + 10, Mouse.Location.Y + 10); //First item in the menu
            Mouse.Click();
        }

        public void ActivateEndpointExplorer()
        {
            var endpointExplorer = barManager.Get<GroupBox>(SearchCriteria.ByClassName("LayoutPanel").AndAutomationId("EndpointExplorer"));

            endpointExplorer.ShouldNotBe(null);

            Dock(endpointExplorer);
        }

        public void SelectFlowDiagramTab()
        {
            SelectTab("MessageFlow");
        }

        public void SelectSagaTab()
        {
            SelectTab("SagaWindow");
        }

        public void SelectHeadersTab()
        {
            SelectTab("MessageHeaders");
        }

        public void SelectBodyTab()
        {
            SelectTab("MessageBody");
        }

        public void SelectLogsTab()
        {
            SelectTab("LogWindow");
        }

        private void SelectTab(string automationId)
        {
            var tabbedGroup = barManager.Get(SearchCriteria.ByControlType(ControlType.Tab).AndAutomationId("MainTabbedView"));
            tabbedGroup.ShouldNotBe(null);

            var tabToSelect = tabbedGroup.Get<Button>(automationId + "TabButtonId");
            tabToSelect.ShouldNotBe(null);

            tabToSelect.Click();
        }
    }
}