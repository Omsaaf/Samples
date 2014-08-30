﻿using SolidEdgeCommunity.Extensions; // Enabled extension methods from SolidEdge.Community.dll
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSamples.SheetMetal
{
    /// <summary>
    /// Creates a new sheetmetal with a base tab.
    /// </summary>
    class CreateBaseTab
    {
        static void RunSample(bool breakOnStart)
        {
            if (breakOnStart) System.Diagnostics.Debugger.Break(); 
            
            SolidEdgeFramework.Application application = null;
            SolidEdgeFramework.Documents documents = null;
            SolidEdgePart.SheetMetalDocument sheetMetalDocument = null;
            SolidEdgePart.Model model = null;
            SolidEdgePart.Tabs tabs = null;
            SolidEdgePart.Tab tab = null;
            SolidEdgeFramework.SelectSet selectSet = null;

            try
            {
                // Register with OLE to handle concurrency issues on the current thread.
                SolidEdgeCommunity.OleMessageFilter.Register();

                // Connect to or start Solid Edge.
                application = SolidEdgeCommunity.SolidEdgeUtils.Connect(true, true);

                // Bring Solid Edge to the foreground.
                application.Activate();

                // Get a reference to the Documents collection.
                documents = application.Documents;

                // Create a new SheetMetal document.
                sheetMetalDocument = documents.AddSheetMetalDocument();

                // Always a good idea to give SE a chance to breathe.
                application.DoIdle();

                // Call helper method to create the actual geometry.
                model = SheetMetalHelper.CreateBaseTab(sheetMetalDocument);

                // Get a reference to the Tabs collection.
                tabs = model.Tabs;

                // Get a reference to the new Tab.
                tab = tabs.Item(1);

                // Get a reference to the ActiveSelectSet.
                selectSet = application.ActiveSelectSet;

                // Empty ActiveSelectSet.
                selectSet.RemoveAll();

                // Add new Tab to ActiveSelectSet.
                selectSet.Add(tab);

                // Switch to ISO view.
                application.StartCommand(SolidEdgeConstants.SheetMetalCommandConstants.SheetMetalViewISOView);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SolidEdgeCommunity.OleMessageFilter.Unregister();
            }
        }
    }
}
