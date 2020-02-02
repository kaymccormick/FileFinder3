﻿using System ;
using System.Collections.Generic ;
using System.Diagnostics.CodeAnalysis ;
using System.Linq ;
using System.Reflection ;
using AppShared ;
using AppShared.Interfaces ;
using NLog ;

namespace Common.Context
{
    /// <summary></summary>
    /// <seealso cref="System.Attribute" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for PushContextAttribute
    public class PushContextAttribute : Attribute

    {
        [ SuppressMessage ( "ReSharper" , "InternalOrPrivateMemberNotDocumented" ) ] private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        private InfoContext                  _context ;
        private ContextStack < InfoContext > _stack ;



        /// <summary>Initializes a new instance of the <see cref="PushContextAttribute"/> class.</summary>
        /// <param name="context">The context.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public PushContextAttribute ( object context ) { Context = context ; }

        /// <summary>Initializes a new instance of the <see cref="PushContextAttribute"/> class.</summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public PushContextAttribute ( string v1 , string v2 )
        {
            Context = new InfoContext ( v1 , v2 ) ;
        }

        /// <summary>Gets the context.</summary>
        /// <value>The context.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Context
        public object Context { get ; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        /// <summary>Gets or sets the instances.</summary>
        /// <value>The instances.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Instances
        public IEnumerable < KeyValuePair < object , long > > Instances { get ; set ; }

        /// <summary>
        ///     This method is called after the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public void After ( MethodInfo methodUnderTest )
        {
            // Assert.NotEmpty( _stack );
            // Assert.True( Object.ReferenceEquals( _stack.First(), _context ) );
            _stack.Pop ( ) ;
        }

        /// <summary>
        ///     This method is called before the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public void Before ( MethodInfo methodUnderTest )
        {
            //
            // var genProps =
            //     from prop in instance.GetType().GetProperties()
            //     let atts = Attribute.GetCustomAttributes(prop, typeof(InfoContextFactoryAttribute))
            //     from ContextStackInstanceAttribute att in atts
            //     select new { Prop = prop, Att = att };
            // Assert.Single(factoryProps);
            // var entryFactory = factoryProps.First().Prop.GetValue(instance) as InfoContext.Factory;



            var instances =
                Instances.Where ( o => o.Key.GetType ( ) == methodUnderTest.DeclaringType ) ;
            // Assert.Single( instances );
            var instance = instances.Last ( ).Key ;
            //var qq = from prop in instance.GetType().GetProperties() select Attribute.GetCustomAttributes(prop, typeof(ContextStackInstanceAttribute)) 

            var factoryProps =
                from prop in instance.GetType ( ).GetProperties ( )
                let atts = GetCustomAttributes ( prop , typeof ( InfoContextFactoryAttribute ) )
                from InfoContextFactoryAttribute att in atts
                select new { Prop = prop , Att = att } ;
            // Assert.Single(factoryProps);
            var entryFactory =
                factoryProps.First ( ).Prop.GetValue ( instance ) as InfoContext.Factory ;

            Logger.Debug ( "instance is {instance}" ) ;
            if ( instance is IHasId hasId )
            {
                Logger.Debug ( $"id is {hasId.ObjectId}" ) ;
            }

            var qq =
                from prop in instance.GetType ( ).GetProperties ( )
                let atts = GetCustomAttributes ( prop , typeof ( ContextStackInstanceAttribute ) )
                from ContextStackInstanceAttribute att in atts
                select new { Prop = prop , Att = att } ;
            // Assert.NotEmpty(qq);
            foreach ( var q in qq )
            {
                Logger.Debug ( $"{q.Prop} {q.Att}" ) ;
                var value = q.Prop.GetValue ( instance ) ;
                if ( value is ContextStack < InfoContext > stack )
                {
                    // thread safe?
                    _context = entryFactory ( "test1" , Context ) ;
                    _stack   = stack ;
                    _stack.Push ( _context ) ;
                }
            }

            //Assert.NotNull( _stack );

            //var contextAtt = Attribute.GetCustomAttributes(instance.GetType(),)
            var customAttributes = GetCustomAttributes ( methodUnderTest , GetType ( ) ) ;
            foreach ( var q in customAttributes )
            {
            }
        }
    }
}