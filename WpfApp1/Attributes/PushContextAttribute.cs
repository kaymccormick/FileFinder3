﻿using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Reflection ;
using AppShared.Interfaces ;
using Microsoft.Scripting.Utils ;
using NLog ;
using WpfApp1.Util ;

namespace WpfApp1.Attributes
{
    public class PushContextAttribute : Attribute

    {
        public object Context { get; }

        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        private InfoContext                  _context;
        private ContextStack < InfoContext > _stack;
        private string v1;
        private string v2;


        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute" /> class.</summary>
        public PushContextAttribute(
            object context
        )
        {
            Context = context;
        }

        public PushContextAttribute ( string v1 , string v2 )
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        /// <summary>
        /// This method is called after the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public void After(
            MethodInfo methodUnderTest
        )
        {
            // Assert.NotEmpty( _stack );
            // Assert.True( Object.ReferenceEquals( _stack.First(), _context ) );
            _stack.Pop();
        }

        /// <summary>
        /// This method is called before the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public void Before(
            MethodInfo methodUnderTest
        )
        {
            //
            // var genProps =
            //     from prop in instance.GetType().GetProperties()
            //     let atts = Attribute.GetCustomAttributes(prop, typeof(InfoContextFactoryAttribute))
            //     from ContextStackInstanceAttribute att in atts
            //     select new { Prop = prop, Att = att };
            // Assert.Single(factoryProps);
            // var entryFactory = factoryProps.First().Prop.GetValue(instance) as InfoContext.Factory;



            var instances = Instances.Where < KeyValuePair < object , long > > ( o => o.Key.GetType() == methodUnderTest.DeclaringType );
            // Assert.Single( instances );
            var instance = instances.Last().Key;
            //var qq = from prop in instance.GetType().GetProperties() select Attribute.GetCustomAttributes(prop, typeof(ContextStackInstanceAttribute)) 

            var factoryProps =
                from prop in instance.GetType().GetProperties()
                let atts = Attribute.GetCustomAttributes(prop, typeof(InfoContextFactoryAttribute))
                from InfoContextFactoryAttribute att in atts
                select new { Prop = prop, Att = att };
            // Assert.Single(factoryProps);
            var entryFactory = factoryProps.First().Prop.GetValue( instance ) as InfoContext.Factory;

            Logger.Debug("instance is {instance}"  );
            if ( instance is IHasId hasId )
            {
                Logger.Debug($"id is {hasId.ObjectId}"  );
            }
            var qq =
                from prop in instance.GetType()
									 .GetProperties()
                let atts = Attribute.GetCustomAttributes( prop, typeof(ContextStackInstanceAttribute) )
                from ContextStackInstanceAttribute att in atts
                select new { Prop = prop, Att = att };
            // Assert.NotEmpty(qq);
            foreach ( var q in qq )
            {
                Logger.Debug( $"{q.Prop} {q.Att}" );
                var value = q.Prop.GetValue( instance );
                if ( value is ContextStack < InfoContext > stack )
                {
                    // thread safe?
                    _context = entryFactory( "test1", Context );
                    _stack   = stack;
                    _stack.Push( _context );
                }
            }

            Assert.NotNull( _stack );

            //var contextAtt = Attribute.GetCustomAttributes(instance.GetType(),)
            var customAttributes = Attribute.GetCustomAttributes( methodUnderTest, GetType() );
            foreach ( var q in customAttributes )
            {
            }
        }

        public IEnumerable < KeyValuePair < object , long > > Instances { get ; set ; }
    }
}