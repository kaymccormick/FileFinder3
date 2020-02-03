using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.IO ;
using System.Linq ;
using System.Reactive.Concurrency ;
using System.Reactive.Linq ;
using System.Runtime.CompilerServices ;
using System.Threading ;
using System.Threading.Tasks ;
using System.Windows ;
using System.Windows.Automation ;
using System.Windows.Threading ;
using AppShared ;
using Common ;
using Common.Controls ;
using NLog ;
using TestLib.Attributes ;
using TestLib.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;

namespace CommonTests
{
    /// <summary>
    ///     Test class for tests of <see cref="TypeControl" />
    /// </summary>
    [ LogTestMethod ]
    public class TypeControlTests : IClassFixture < LoggingFixture >
    {
        private readonly ITestOutputHelper _originalOutput ;
        private readonly ITestOutputHelper _output ;

        /// <summary>
        ///     Constructor for test class
        /// </summary>
        /// <param name="output"></param>
        public TypeControlTests ( ITestOutputHelper output )
        {
            _originalOutput = output ;
            _output         = new OutputHelperWrapper ( output ) ;
        }


        /// <summary>
        ///     Test Type Control
        /// </summary>
        /// <exception cref="AggregateException"></exception>
        [ WpfFact ]
        [ Trait ( "UITest" , "true" ) ]
        protected void TestTypeControl ( )
        {
            SetupCacheSubscriber ( ) ;
            var controlName = SetupTypeControl ( out var control ) ;
            control.SetValue ( App.RenderedTypeProperty , typeof ( string ) ) ;
            control.Detailed = true ;

            var window = MakeWindow ( control , out var taskCompletionSource ) ;
            _output.WriteLine ( "showing window" ) ;
            window.Show ( ) ;
            _output.WriteLine ( "Asserting that the task completion source result is not null." ) ;
            Assert.NotNull ( taskCompletionSource.Task.Result ) ;
            _output.WriteLine ( "Assertion complete." ) ;
            if ( taskCompletionSource.Task.IsFaulted )
            {
                _output.WriteLine ( "task faulted" ) ;
                if ( taskCompletionSource.Task.Exception != null )
                {
                    throw taskCompletionSource.Task.Exception ;
                }
            }

            Task.Factory.StartNew (
                                   ( ) => {
                                       var autoElem = FindControlAutomationElement ( controlName ) ;
                                       Assert.NotNull ( autoElem ) ;

                                       var hyperlinks = FindHyperlinks ( autoElem ) ;

                                       Automation.AddStructureChangedEventHandler (
                                                                                   autoElem
                                                                                 , TreeScope
                                                                                      .Descendants
                                                                                 , ( sender , args )
                                                                                       => {
                                                                                       _output
                                                                                          .WriteLine (
                                                                                                      $"structure: {args.StructureChangeType}"
                                                                                                     ) ;
                                                                                   }
                                                                                  ) ;


                                       Assert.NotEmpty ( hyperlinks ) ;
                                       foreach ( AutomationElement hyperlink in hyperlinks )
                                       {
                                           // ReSharper disable once UnusedVariable
                                           var linkText =
                                               hyperlink.GetCurrentPropertyValue (
                                                                                  AutomationElement
                                                                                     .NameProperty
                                                                                 ) ;
                                           if ( hyperlink.TryGetCurrentPattern (
                                                                                InvokePattern
                                                                                   .Pattern
                                                                              , out
                                                                                var patternObject
                                                                               ) )
                                           {
                                               if ( patternObject is InvokePattern invoke )
                                               {
                                                   Automation
                                                      .AddAutomationPropertyChangedEventHandler (
                                                                                                 hyperlink
                                                                                               , TreeScope
                                                                                                    .Element
                                                                                               , (
                                                                                                     sender
                                                                                                   , args
                                                                                                 ) => {
                                                                                                     _output
                                                                                                        .WriteLine (
                                                                                                                    "update: "
                                                                                                                    + args
                                                                                                                     .Property
                                                                                                                     .ProgrammaticName
                                                                                                                    + " = "
                                                                                                                    + args
                                                                                                                       .NewValue
                                                                                                                   ) ;
                                                                                                 }
                                                                                               , hyperlink
                                                                                                    .GetSupportedProperties ( )
                                                                                                ) ;

                                                   _output.WriteLine ( "yay" ) ;
                                                   invoke.Invoke ( ) ;
                                                   Thread.Sleep ( 2000 ) ;
                                               }
                                           }
                                       }
                                   }
                                 , TaskCreationOptions.DenyChildAttach
                                  )
                .Wait ( 5000 ) ;
        }

        /// <summary>Tests the type navigator.</summary>
        /// <exception cref="Exception"></exception>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestTypeNavigator
        [ WpfFact ]
        [ Trait ( "UITest" , "true" ) ]
        private void TestTypeNavigator ( )
        {
            SetupCacheSubscriber ( ) ;

            var controlName = SetupTypeNavControl ( out var control ) ;
            var window = MakeWindow ( control , out var r ) ;
            window.Show ( ) ;
            Assert.NotNull ( r.Task.Result ) ;
            if ( r.Task.IsFaulted )
            {
                if ( r.Task.Exception != null )
                {
                    throw r.Task.Exception ;
                }

                throw new Exception ( ) ;
            }


            var autoElem = FindControlAutomationElement ( controlName ) ;
            Assert.NotNull ( autoElem ) ;

            var hyperlinks = FindHyperlinks ( autoElem ) ;

            Automation.AddStructureChangedEventHandler (
                                                        autoElem
                                                      , TreeScope.Descendants
                                                      , StructureChangedEventHandler
                                                       ) ;


            Assert.NotEmpty ( hyperlinks ) ;
            foreach ( AutomationElement hyperlink in hyperlinks )
            {
                // ReSharper disable once UnusedVariable
                var linkText =
                    hyperlink.GetCurrentPropertyValue ( AutomationElement.NameProperty ) ;
                if ( DoInvokeHyperlink ( hyperlink ) )
                {
                    break ;
                }
            }
        }

        /// <summary>Does the invoke hyperlink.</summary>
        /// <param name="hyperlink">The hyperlink.</param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DoInvokeHyperlink
        private bool DoInvokeHyperlink ( AutomationElement hyperlink )
        {
            if ( hyperlink.TryGetCurrentPattern ( InvokePattern.Pattern , out var patternObject ) )
            {
                if ( patternObject is InvokePattern invoke )
                {
                    Automation.AddAutomationPropertyChangedEventHandler (
                                                                         hyperlink
                                                                       , TreeScope.Element
                                                                       , ( sender , args ) => {
                                                                             _output.WriteLine (
                                                                                                "update: "
                                                                                                + args
                                                                                                 .Property
                                                                                                 .ProgrammaticName
                                                                                                + " = "
                                                                                                + args
                                                                                                   .NewValue
                                                                                               ) ;
                                                                         }
                                                                       , hyperlink
                                                                            .GetSupportedProperties ( )
                                                                        ) ;

                    _output.WriteLine ( "yay" ) ;
                    invoke.Invoke ( ) ;
                    Thread.Sleep ( 2000 ) ;
                    return true ;
                }
            }

            return false ;
        }

        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private void StructureChangedEventHandler ( object sender , StructureChangedEventArgs args )
        {
            _output.WriteLine ( $"structure: {args.StructureChangeType}" ) ;
        }


        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private AutomationElementCollection FindHyperlinks ( AutomationElement autoElem )
        {
            _output.WriteLine ( "About to find hyperlinks" ) ;

            var hyperlinks = autoElem.FindAll (
                                               TreeScope.Descendants
                                             , new PropertyCondition (
                                                                      AutomationElement
                                                                         .ClassNameProperty
                                                                    , "Hyperlink"
                                                                     )
                                              ) ;
            _output.WriteLine ( $"Found {hyperlinks.Count} hyperlinks" ) ;
            return hyperlinks ;
        }

        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private AutomationElement FindControlAutomationElement ( string controlName )
        {
            _output.WriteLine ( "About to find automation element" ) ;
            var first = AutomationElement.RootElement.FindFirst (
                                                                 TreeScope.Children
                                                               , new PropertyCondition (
                                                                                        AutomationElement
                                                                                           .AutomationIdProperty
                                                                                      , "MYWin"
                                                                                       )
                                                                ) ;
            _output.WriteLine ( "Found automation element " + first ) ;
            Assert.NotNull ( first ) ;

            _output.WriteLine (
                               "Trying to find control with Automation ID property " + controlName
                              ) ;
            var autoElem = first.FindFirst (
                                            TreeScope.Descendants
                                          , new PropertyCondition (
                                                                   AutomationElement
                                                                      .AutomationIdProperty
                                                                 , controlName
                                                                  )
                                           ) ;
            _output.WriteLine ( "Found automation element " + autoElem ) ;
            return autoElem ;
        }

        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private Window MakeWindow (
            UIElement                           control
          , out TaskCompletionSource < Result > taskCompletionSource
        )
        {
            var window = new Window { Name = "MYWin" , Content = control } ;
            taskCompletionSource = new TaskCompletionSource < Result > ( ) ;

            var source = taskCompletionSource ;
            window.Loaded += ( sender , args ) => {
                try
                {
                    _output.WriteLine ( "Window loaded." ) ;
                    var res = new Result ( true ) ;
                    throw new TestException ( ) ;
                    _output.WriteLine ( "Setting task source." ) ;
                    source.SetResult ( res ) ;
                }
                catch ( Exception ex )
                {
                    _output.WriteLine ( $"Exception: {ex.Message}." ) ;
                    source.TrySetException ( ex ) ;
                }
            } ;
            return window ;
        }

        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private string SetupTypeNavControl ( out TypeNavigator control )
        {
            var controlName = "typeNav" ;
            control = new TypeNavigator { Name = controlName } ;
            control.SetValue (
                              App.RenderedTypeProperty
                            , typeof ( Dictionary < string , List < Tuple < int , object > > > )
                             ) ;
            return controlName ;
        }

        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private string SetupTypeControl ( out TypeControl control )
        {
            var controlName = "typeControl" ;
            control = new TypeControl { Name = controlName } ;
            control.SetValue (
                              App.RenderedTypeProperty
                            , typeof ( Dictionary < string , List < Tuple < int , object > > > )
                             ) ;
            return controlName ;
        }

        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private void SetupCacheSubscriber ( )
        {
            var myCacheTarget = MyCacheTarget.GetInstance ( 1000 ) ;
            _output.WriteLine ( $"{nameof ( myCacheTarget )} is {myCacheTarget.Name}" ) ;
            myCacheTarget.Cache.SubscribeOn ( Scheduler.Default )
                         .Buffer ( TimeSpan.FromMilliseconds ( 100 ) )
                         .Where ( x => x.Any ( ) )
                         .ObserveOnDispatcher ( DispatcherPriority.Background )
                         .Subscribe (
                                     infos => {
                                         foreach ( var logEventInfo in infos )
                                         {
                                             _originalOutput.WriteLine (
                                                                        logEventInfo
                                                                           .FormattedMessage
                                                                       ) ;
                                         }
                                     }
                                    ) ;
        }

        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        // ReSharper disable once UnusedMember.Local
        private void WalkContentElements ( AutomationElement autoElem , bool b )
        {
            var elementNode = TreeWalker.ContentViewWalker.GetFirstChild ( autoElem ) ;

            while ( elementNode != null )
            {
                if ( b )
                {
                    foreach ( var automationProperty in elementNode.GetSupportedProperties ( ) )
                    {
                        _output.WriteLine (
                                           $"{automationProperty.ProgrammaticName}: {elementNode.GetCurrentPropertyValue ( automationProperty )}"
                                          ) ;
                    }
                }

                try
                {
                    var automationId =
                        elementNode.GetCurrentPropertyValue (
                                                             AutomationElement.AutomationIdProperty
                                                            ) ;
                    _output.WriteLine ( automationId?.ToString ( ) ) ;
                }
                catch ( Exception ex )
                {
                    _output.WriteLine ( ex.Message ) ;
                }

                WalkContentElements ( elementNode , b ) ;
                elementNode = TreeWalker.ContentViewWalker.GetNextSibling ( elementNode ) ;
            }
        }


        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        // ReSharper disable once UnusedMember.Local
        private void WalkControlElements ( AutomationElement rootElement , bool dumpProps )
        {
            // Conditions for the basic views of the subtree (content, control, and raw) 
            // are available as fields of TreeWalker, and one of these is used in the 
            // following code.
            var elementNode = TreeWalker.ControlViewWalker.GetFirstChild ( rootElement ) ;

            while ( elementNode != null )
            {
                if ( dumpProps )
                {
                    foreach ( var automationProperty in elementNode.GetSupportedProperties ( ) )
                    {
                        _output.WriteLine (
                                           $"{automationProperty.ProgrammaticName}: {elementNode.GetCurrentPropertyValue ( automationProperty )}"
                                          ) ;
                    }
                }

                try
                {
                    var automationId =
                        elementNode.GetCurrentPropertyValue (
                                                             AutomationElement.AutomationIdProperty
                                                            ) ;
                    _output.WriteLine ( automationId?.ToString ( ) ) ;
                }
                catch ( Exception ex )
                {
                    _output.WriteLine ( ex.Message ) ;
                }

                WalkControlElements ( elementNode , dumpProps ) ;
                elementNode = TreeWalker.ControlViewWalker.GetNextSibling ( elementNode ) ;
            }
        }
    }

    /// <summary>
    /// </summary>
    internal class TestException : Exception
    {
    }

#pragma warning disable 1591
    public class OutputHelperWrapper : ITestOutputHelper
#pragma warning restore 1591
    {
        private readonly Logger            _logger ;
        private readonly ITestOutputHelper _output ;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="OutputHelperWrapper" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="filePath">The file path.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public OutputHelperWrapper (
            ITestOutputHelper         output
          , [ CallerFilePath ] string filePath = ""
        )
        {
            _output = output ;
            _logger = LogManager.LogFactory.GetLogger < Logger > (
                                                                  Path.GetFileNameWithoutExtension (
                                                                                                    filePath
                                                                                                   )
                                                                 ) ;
        }

        /// <summary>Adds a line of text to the output.</summary>
        /// <param name="message">The message</param>
        public void WriteLine ( string message )
        {
            _output.WriteLine ( message ) ;
            Debug.WriteLine ( message ) ;
            _logger.Debug ( message ) ;
        }

        /// <summary>Formats a line of text and adds it to the output.</summary>
        /// <param name="format">The message format</param>
        /// <param name="args">The format arguments</param>
        public void WriteLine ( string format , params object[] args )
        {
            _output.WriteLine ( format , args ) ;
            Debug.WriteLine ( format , args ) ;
            _logger.Debug ( format , args ) ;
        }
    }

    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for Result
    public class Result
    {
        /// <summary>The success</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Success
        public bool Success ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public Result ( bool success ) { Success = success ; }

        /// <summary>Initializes a new instance of the <see cref="Result"/> class.</summary>
        /// <param name="autoElem">The automatic elem.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public Result ( AutomationElement autoElem ) { AutoElem = autoElem ; }

        /// <summary>
        /// </summary>
        public AutomationElement AutoElem { get ; set ; }
    }
}