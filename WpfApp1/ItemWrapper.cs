using System;
using System.Collections.Generic;
using System.Diagnostics;
using NLog;
using Vanara.Windows.Shell;

namespace WpfApp1
{
    public class ItemWrapper
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public ItemWrapper(
            ShellItem item
        )
        {
            Name        = item.Name;
            ParsingName = item.ParsingName;
            var store = item.Properties;
            Attributes     = item.Attributes;
            Props          = new Dictionary < object, object >();
            FileInfo       = item.FileInfo;
            FileSystemPath = item.FileSystemPath;
            IsFileSystem   = item.IsFileSystem;
            IsFolder       = item.IsFolder;
            IShellItem     = item.IShellItem;
            IsLink         = item.IsLink;
            Parent         = item.Parent;
            PIDL           = item.PIDL;
            ToolTipText    = item.ToolTipText;

            foreach ( var key in store.Keys )
            {
                var propertyDescription = store.Descriptions[key];
                try
                {
                    var propertyDescriptionList =
                        item.GetPropertyDescriptionList( key );
                    Debug.Assert( propertyDescriptionList != null );
                    foreach ( var v in propertyDescriptionList )
                    {
                        Logger.Debug( $"{v}" );
                    }

                    //.Where(description => description.PropertyKey == key).First();
                    //Props[propertyDescription.CanonicalName] = store[key];
                }
                catch ( Exception e )
                {
                    Logger.Warn( e, $"{key} - {e.Message}" );
                }
            }
        }

        public string ToolTipText { get; set; }

        public Vanara.PInvoke.Shell32.PIDL PIDL { get; set; }

        public bool IsLink { get; set; }

        public ShellFolder Parent { get; set; }

        public bool IsFolder { get; set; }

        public Vanara.PInvoke.Shell32.IShellItem IShellItem { get; set; }

        public bool IsFileSystem { get; set; }

        public string FileSystemPath { get; set; }

        public ShellFileInfo FileInfo { get; set; }

        public ShellItemAttribute Attributes { get; set; }

        public Dictionary < object, object > Props { get; set; }

        public string ParsingName { get; set; }

        public string Name { get; set; }
    }
}