﻿using AppShared ;
using AppShared.Infos ;

namespace TestLib
{
    /// <summary></summary>
    /// <seealso cref="TestLib.IMyServices" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for MyServices
    public class MyServices : IMyServices
    {
        /// <summary>Initializes a new instance of the <see cref="MyServices"/> class.</summary>
        /// <param name="infoContextFactory">The information context factory.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public MyServices ( InfoContext.Factory infoContextFactory )
        {
            InfoContextFactory = infoContextFactory ;
        }

        /// <summary>Gets the information context factory.</summary>
        /// <value>The information context factory.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for InfoContextFactory
        public InfoContext.Factory InfoContextFactory { get ; }
    }
}