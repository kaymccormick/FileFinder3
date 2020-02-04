﻿using System ;
using AppShared.Interfaces ;
using Autofac ;
using NLog ;
using TestLib ;
using TestLib.Attributes ;
using TestLib.Fixtures ;
using WpfApp1.Menus ;
using Xunit ;
using Xunit.Abstractions ;

namespace WpfApp1Tests3
{
    /// <summary></summary>
    [Collection ( "WpfApp" ) ]
    [LogTestMethod, BeforeAfterLogger]
    public class MenuHelperTests : WpfTestsBase
    {
        // ReSharper disable once UnusedMember.Local
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        private readonly Func < IMenuItem > _xMenuItemCreator ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public MenuHelperTests (
            WpfApplicationFixture fixture
          , AppContainerFixture containerFixture
          , UtilsContainerFixture utilsContainerFixture
          , ITestOutputHelper outputHelper
        ) : base (
                  fixture
                , containerFixture
                , utilsContainerFixture
                , outputHelper
                 )
        {
            _xMenuItemCreator = containerScope.Resolve < Func < IMenuItem > > ( ) ;
        }

        /// <summary>Makes the menu item test.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for MakeMenuItemTest
        [WpfFact ]
        public void MakeMenuItemTest ( )
        {
            var header = "test" ;
            var arg = _xMenuItemCreator ( ) ;
            arg.Header = header ;
            Assert.NotNull ( arg ) ;
            var item = MenuHelper.MakeMenuItem ( arg ) ;
            Assert.NotNull ( item ) ;
            Assert.Equal ( header , item.Header ) ;
        }
    }
}