﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency ;
using System.Reactive.Linq ;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Threading ;
using AppShared ;
using Common;
using Common.Controls;
using CommonTests.Fixtures ;
using TestLib ;
using Xunit ;
using Xunit.Abstractions ;

namespace CommonTests
{
    /// <summary>
    /// Test class for tests of <see cref="TypeControl"/>
    /// </summary>
    public class TypeControlTests : IClassFixture<LoggingFixture>
    {
        private readonly ITestOutputHelper _output ;

        public TypeControlTests ( ITestOutputHelper output ) { _output = output ; }

        [ WpfFact ]
        public void TestTypeControl1 ( )
        {
            SetupCacheSubscriber ( ) ;
            var controlName = "MyControl" ;
            var control = new TypeControl { Name = controlName } ;
            control.SetValue(App.RenderedTypeProperty, typeof( string ) );
            control.Detailed     = true ;
            var window = MakeWindow ( control , out var r ) ;
            window.Show ( ) ;
            Assert.NotNull ( r.Task.Result ) ;
            if ( r.Task.IsFaulted )
            {
                throw r.Task.Exception ;
            }

            var e2 = r.Task.Result.AutoElem ;
            var p = new[]
                    {
                        AutomationElement.NameProperty , AutomationElement.ControlTypeProperty
                      , AutomationElement.FrameworkIdProperty
                      , AutomationElement.IsWindowPatternAvailableProperty
                    } ;
            foreach ( var o1 in p.Select (
                                          ( property , i ) => {
                                              try
                                              {
                                                  return new Tuple < AutomationProperty , object > (
                                                                                                    property
                                                                                                  , e2
                                                                                                       .GetCurrentPropertyValue (
                                                                                                                                 property
                                                                                                                                )
                                                                                                   ) ;
                                              }
                                              catch ( Exception ex )
                                              {
                                                  _output.WriteLine (
                                                                     $"{property.ProgrammaticName} - {ex.Message}"
                                                                    ) ;
                                              }

                                              return null ;
                                          }
                                         ) )
            {
                if ( o1 != null )
                {
                    _output.WriteLine ( o1.Item1.ProgrammaticName + ": " + o1.Item2 ) ;
                }
            }

            foreach ( var automationPattern in e2.GetSupportedPatterns ( ) )
            {
                _output.WriteLine ( "pattern is " + automationPattern.ProgrammaticName ) ;
            }

            var controlae = FindControlA ( e2 , controlName ) ;
            Assert.NotNull ( controlae ) ;

            var hyperlinks = FindHyperlinks ( controlae ) ;

//            WalkContentElements(controlae, true);
            WalkControlElements(controlae, true);
            Automation.AddStructureChangedEventHandler(controlae, TreeScope.Descendants, (
                                                           sender
                                                         , args
                                                       ) => {
                                                           _output.WriteLine (
                                                                              $"structure: {args.StructureChangeType}"
                                                                             ) ;
                                                       });
            

            Assert.NotEmpty(hyperlinks);
            // Assert.True ( hyperlinks.Count > 2 ) ;
            foreach ( AutomationElement hyperlink in hyperlinks )
            {
                Point pt ;
                var v = hyperlink.TryGetClickablePoint ( out pt ) ;
                var linkText =
                    hyperlink.GetCurrentPropertyValue ( AutomationElement.NameProperty ) ;
                if ( hyperlink.TryGetCurrentPattern (
                                                     InvokePattern.Pattern
                                                   , out var patternObject
                                                    ) )
                {
                    if ( patternObject is InvokePattern inboke )
                    {
                        Automation.AddAutomationPropertyChangedEventHandler (
                                                                            hyperlink
                                                                          , TreeScope.Element
                                                                          , ( sender , args ) => {

                                                                                _output.WriteLine ("update: " + 
                                                                                                   args
                                                                                                      .Property
                                                                                                      .ProgrammaticName + " = " + args.NewValue
                                                                                                  ) ;
                                                                            }
             ,hyperlink.GetSupportedProperties()                                            ) ;

                        _output.WriteLine ( "yay" ) ;
                        inboke.Invoke ( ) ;
                        Thread.Sleep ( 2000 ) ;

                        // propChangeHandler = new AutomationPropertyChangedEventHandler(OnPropertyChange),
                        // AutomationElement.IsEnabledProperty);
                        // Automation.AddAutomationEventHandler();
                        // }

                        // _output.WriteLine ( $"{patternObject}" ) ;
                    }

                }

                if ( v )
                {
                    _output.WriteLine ( $"{linkText}: {pt}" ) ;
                    foreach ( var automationPattern in hyperlink.GetSupportedPatterns ( ) )
                    {
                        _output.WriteLine ( $"{automationPattern.ProgrammaticName}" ) ;
                        var currentPattern = hyperlink.GetCurrentPattern ( automationPattern ) ;
                        _output.WriteLine ( $"{currentPattern}" ) ;
                    }
                }
            }


            // WalkContentElements ( controlae , true) ;
            // var clickablePoint = controlae.GetClickablePoint ( ) ;
            // _output.WriteLine ( $"{clickablePoint}" ) ;

            //WalkControlElements (controlae , true ) ;

            //WalkContentElements()
        }

         [ WpfFact ]
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
            }

            var e2 = r.Task.Result.AutoElem ;

            var controlae = FindControlA ( e2 , controlName ) ;
            Assert.NotNull ( controlae ) ;

            var hyperlinks = FindHyperlinks ( controlae ) ;

//            WalkContentElements(controlae, true);
            // WalkControlElements(controlae, true);
            Automation.AddStructureChangedEventHandler(controlae, TreeScope.Descendants, (
                                                           sender
                                                         , args
                                                       ) => {
                                                           _output.WriteLine (
                                                                              $"structure: {args.StructureChangeType}"
                                                                             ) ;
                                                       });
            

            Assert.NotEmpty(hyperlinks);
            foreach ( AutomationElement hyperlink in hyperlinks )
            {
                Point pt ;
                var v = hyperlink.TryGetClickablePoint ( out pt ) ;
                var linkText =
                    hyperlink.GetCurrentPropertyValue ( AutomationElement.NameProperty ) ;
                if ( hyperlink.TryGetCurrentPattern (
                                                     InvokePattern.Pattern
                                                   , out var patternObject
                                                    ) )
                {
                    if ( patternObject is InvokePattern inboke )
                    {
                        Automation.AddAutomationPropertyChangedEventHandler (
                                                                            hyperlink
                                                                          , TreeScope.Element
                                                                          , ( sender , args ) => {

                                                                                _output.WriteLine ("update: " + 
                                                                                                   args
                                                                                                      .Property
                                                                                                      .ProgrammaticName + " = " + args.NewValue
                                                                                                  ) ;
                                                                            }
             ,hyperlink.GetSupportedProperties()                                            ) ;

                        _output.WriteLine ( "yay" ) ;
                        inboke.Invoke ( ) ;
                        Thread.Sleep ( 2000 ) ;
                        break ;
                    }

                }

                if ( v )
                {
                    _output.WriteLine ( $"{linkText}: {pt}" ) ;
                    foreach ( var automationPattern in hyperlink.GetSupportedPatterns ( ) )
                    {
                        _output.WriteLine ( $"{automationPattern.ProgrammaticName}" ) ;
                        var currentPattern = hyperlink.GetCurrentPattern ( automationPattern ) ;
                        _output.WriteLine ( $"{currentPattern}" ) ;
                    }
                }
            }

        }

        private static AutomationElementCollection FindHyperlinks ( AutomationElement controlae )
        {
            var hyperlinks = controlae.FindAll (
                                                TreeScope.Descendants
                                              , new PropertyCondition (
                                                                       AutomationElement.ClassNameProperty
                                                                     , "Hyperlink"
                                                                      )
                                               ) ;
            return hyperlinks ;
        }

        private static AutomationElement FindControlA ( AutomationElement e2 , string controlName )
        {
            var controlae = e2.FindFirst (
                                          TreeScope.Descendants
                                        , new PropertyCondition (
                                                                 AutomationElement.AutomationIdProperty
                                                               , controlName
                                                                )
                                         ) ;
            return controlae ;
        }

        private Window MakeWindow ( UIElement control , out TaskCompletionSource < Result > taskCompetionSource )
        {
            var w = new Window {
                Name = "MYWin" ,
                Content = control
            };
            taskCompetionSource = new TaskCompletionSource < Result > ( ) ;

            var source = taskCompetionSource ;
            w.Loaded += ( sender , args ) => {
                try
                {
                    // _output.WriteLine ( $"{Thread.CurrentThread.ManagedThreadId}" ) ;
                    // var e = AutomationElement.RootElement.FindFirst (
                    //                                                  TreeScope.Children
                    //                                                , new PropertyCondition (
                    //                                                                         AutomationElement
                    //                                                                            .ProcessIdProperty
                    //                                                                       , Process
                    //                                                                        .GetCurrentProcess ( )
                    //                                                                        .Id
                    //                                                                        )
                    //                                                 ) ;
                    //
                    var rr = new Result();
                    source.SetResult ( rr ) ;
                }
                catch ( Exception ex )
                {
                    source.TrySetException ( ex ) ;
                }
            } ;
            return w ;
        }

        private string SetupTypeNavControl ( out TypeNavigator control )
        {
            var controlName = "typeNav" ;
            control = new TypeNavigator ( ) { Name = controlName } ;
            control.SetValue (
                              AppShared.App.RenderedTypeProperty
                            , typeof ( Dictionary < string , List < Tuple < int , object > > > )
                             ) ;
            return controlName ;
        }

        private void SetupCacheSubscriber ( )
        {
            MyCacheTarget.GetInstance ( 1000 )
                         .Cache.SubscribeOn ( Scheduler.Default )
                         .Buffer ( TimeSpan.FromMilliseconds ( 100 ) )
                         .Where ( x => x.Any ( ) )
                         .ObserveOnDispatcher ( DispatcherPriority.Background )
                         .Subscribe (
                                     infos => {
                                         foreach ( var logEventInfo in infos )
                                         {
                                             _output.WriteLine ( logEventInfo.FormattedMessage ) ;
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

    public class Result
    {
        public AutomationElement AutoElem { get ; set ; }
    }
}
