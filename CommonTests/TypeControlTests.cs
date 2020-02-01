using AppShared ;
using Common ;
using Common.Controls ;
using NLog ;
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
using TestLib ;
using TestLib.Attributes ;
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
        private readonly ITestOutputHelper _output ;
        private readonly ITestOutputHelper originalOutput ;

        /// <summary>
        /// Constructor for test class
        /// </summary>
        /// <param name="output"></param>
        public TypeControlTests ( ITestOutputHelper output )
        {
            originalOutput = output ;
            _output        = new OutputHelperWrapper ( output ) ;
        }


        /// <summary>
        /// Test Type Control
        /// </summary>
        /// <exception cref="AggregateException"></exception>
        [ WpfFact ]
        [Trait("UITest", "true")]
        public void TestTypeControl ( )
        {
            SetupCacheSubscriber ( ) ;
            var controlName = SetupTypeControl ( out var control ) ;
            control.SetValue ( App.RenderedTypeProperty , typeof ( string ) ) ;
            control.Detailed = true ;

            var window = MakeWindow ( control , out var taskCompletionSource ) ;
            _output.WriteLine ( "showing window" ) ;
            window.Show ( ) ;
            _output.WriteLine ( $"Asserting that the task completion source result is not null." ) ;
            Assert.NotNull ( taskCompletionSource.Task.Result ) ;
            _output.WriteLine ( $"Assertion complete." ) ;
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
                                       var controlae =
                                           FindControlAutomationElement ( controlName ) ;
                                       Assert.NotNull ( controlae ) ;

                                       var hyperlinks = FindHyperlinks ( controlae ) ;

                                       Automation.AddStructureChangedEventHandler (
                                                                                   controlae
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
                                           Point pt ;
                                           var v = hyperlink.TryGetClickablePoint ( out pt ) ;
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
                                               if ( patternObject is InvokePattern inboke )
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
                                                   inboke.Invoke ( ) ;
                                                   Thread.Sleep ( 2000 ) ;
                                               }
                                           }

                                       }
                                   }
                                 , TaskCreationOptions.DenyChildAttach
                                  )
                .Wait ( 5000 ) ;
        }

        [ WpfFact ]
        [Trait("UITest", "true")]
        public void TestTypeNavigator ( )
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


            var controlae = FindControlAutomationElement ( controlName ) ;
            Assert.NotNull ( controlae ) ;

            var hyperlinks = FindHyperlinks ( controlae ) ;

            Automation.AddStructureChangedEventHandler (
                                                        controlae
                                                      , TreeScope.Descendants
                                                      , StructureChangedEventHandler
                                                       ) ;


            Assert.NotEmpty ( hyperlinks ) ;
            foreach ( AutomationElement hyperlink in hyperlinks )
            {
                // Point pt ;
                // var v = hyperlink.TryGetClickablePoint ( out pt ) ;
                var linkText =
                    hyperlink.GetCurrentPropertyValue ( AutomationElement.NameProperty ) ;
                if ( DoInvokeHyperlinnk ( hyperlink ) )
                {
                    break ;
                }
            }
        }

        private bool DoInvokeHyperlinnk ( AutomationElement hyperlink )
        {
            if ( hyperlink.TryGetCurrentPattern ( InvokePattern.Pattern , out var patternObject ) )
            {
                if ( patternObject is InvokePattern inboke )
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
                    inboke.Invoke ( ) ;
                    Thread.Sleep ( 2000 ) ;
                    return true ;
                }
            }

            return false ;
        }

        private void StructureChangedEventHandler ( object sender , StructureChangedEventArgs args )
        {
            _output.WriteLine ( $"structure: {args.StructureChangeType}" ) ;
        }


        private AutomationElementCollection FindHyperlinks ( AutomationElement controlae )
        {
            _output.WriteLine ( "About to find hyperlinks" ) ;

            var hyperlinks = controlae.FindAll (
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
            _output.WriteLine ( "Found automation element " + first.ToString ( ) ) ;
            Assert.NotNull ( first ) ;

            _output.WriteLine (
                               "Trying to find control with Automation ID property " + controlName
                              ) ;
            var controlae = first.FindFirst (
                                             TreeScope.Descendants
                                           , new PropertyCondition (
                                                                    AutomationElement
                                                                       .AutomationIdProperty
                                                                  , controlName
                                                                   )
                                            ) ;
            _output.WriteLine ( "Found automation element " + controlae ) ;
            return controlae ;
        }

        private Window MakeWindow (
            UIElement                           control
          , out TaskCompletionSource < Result > taskCompetionSource
        )
        {
            var window = new Window { Name = "MYWin" , Content = control } ;
            taskCompetionSource = new TaskCompletionSource < Result > ( ) ;

            var source = taskCompetionSource ;
            window.Loaded += ( sender , args ) => {
                try
                {
                    _output.WriteLine ( "Window loaded." ) ;
                    var rr = new Result ( ) ;
                    throw new TestException ( ) ;
                    _output.WriteLine ( "Setting task source." ) ;
                    source.SetResult ( rr ) ;
                }
                catch ( Exception ex )
                {
                    _output.WriteLine ( $"Exception: {ex.Message}." ) ;
                    source.TrySetException ( ex ) ;
                }
            } ;
            return window ;
        }

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
                                             originalOutput.WriteLine (
                                                                       logEventInfo.FormattedMessage
                                                                      ) ;
                                         }
                                     }
                                    ) ;
        }

        private void WalkContentElements ( AutomationElement controlae , bool b )
        {
            var elementNode = TreeWalker.ContentViewWalker.GetFirstChild ( controlae ) ;

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
    /// 
    /// </summary>
    internal class TestException : Exception
    {
    }

    public class OutputHelperWrapper : ITestOutputHelper
    {
        private readonly ITestOutputHelper _output ;
        private readonly string            _filePath ;
        private          Logger            _logger ;

        public OutputHelperWrapper (
            ITestOutputHelper         output
          , [ CallerFilePath ] string filePath = ""
        )
        {
            _output   = output ;
            _filePath = filePath ;
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

    public class Result
    {
        public AutomationElement AutoElem { get ; set ; }
    }
}