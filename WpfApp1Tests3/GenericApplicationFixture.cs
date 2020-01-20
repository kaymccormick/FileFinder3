using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JetBrains.Annotations;
using KayMcCormick.Dev.Test.Metadata;
using Xunit;

namespace WpfApp1Tests3
{
    [ UsedImplicitly ]
    public class GenericApplicationFixture : IDisposable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly WpfApplicationHelper _wpfApplicationHelper;

        public Uri BasePackUri
        {
            get => _wpfApplicationHelper.BasePackUri;
            set => _wpfApplicationHelper.BasePackUri = value;
        }

        public Application MyApp
        {
            get => _wpfApplicationHelper.MyApp;
            set => _wpfApplicationHelper.MyApp = value;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GenericApplicationFixture()
        {
            var qq = AppDomain.CurrentDomain.GetAssemblies()
                              .Where( a => Attribute.IsDefined( a, typeof(WpfTestApplicationAttribute) ) );
            var assembly = typeof(WpfApp1.Application.App).Assembly;
            Assert.True( Attribute.IsDefined( assembly, typeof(WpfTestApplicationAttribute) ) );
            _wpfApplicationHelper = new WpfApplicationHelper(assembly);
        }
#if randomcode

        var a = AppDomain.CurrentDomain.GetAssemblies()
                         .Where( assembly => Attribute.IsDefined( assembly, typeof(WpfTestApplicationAttribute) ) );
            //var q = from refA in GetType().Assembly.GetReferencedAssemblies() join a in AppDomain.CurrentDomain.GetAssemblies() on refA equals a.GetName() into aGroup
            var q =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                join refName in this.GetType().Assembly.GetReferencedAssemblies() on assembly.GetName() equals refName
                select assembly;
            var exec = Assembly.GetExecutingAssembly();
            Logger.Error( $"{exec}" );
            exec.GetReferencedAssemblies().Select( name => {
                Logger.Warn( $"{name}" );
                return "";
            } ).ToList();


            foreach ( var q3 in this.GetType().Assembly.GetReferencedAssemblies() )
            {
                if ( q3.Name.StartsWith( "WpfApp1" ) )
                {
                    Logger.Info( "yay" );

                    foreach ( var q2 in AppDomain.CurrentDomain.GetAssemblies() )
                    {
                        if ( q3 == q2.GetName() )
                        {
                            Logger.Info( "match" );
                        }
                        else
                        {
                            if ( q3.ToString() == q2.GetName().ToString() )
                            {
                                Logger.Warn( $"match2 {q3} {q2}" );
                            }

                            if ( AssemblyName.ReferenceMatchesDefinition( q3, q2.GetName() ) )
                            {
                                Logger.Warn( $"error match {q3} {q2}" );
                            }
                        }
                    }
                }
            }

            foreach ( var q2 in AppDomain.CurrentDomain.GetAssemblies() )
            {
                Logger.Error( $"{q2}" );
                foreach ( var qQ3 in this.GetType().Assembly.GetReferencedAssemblies() )
                {
                    if ( qQ3.Name.StartsWith( "WpfApp1" ) )
                    {
                        Logger.Info( "yay" );
                    }

                    Logger.Debug( $"{qQ3.ToString().Substring( 0, 16 ):0,16} {q2.FullName.ToString().Substring( 0, 16 ):0,16}" );
                    if ( qQ3 == q2.GetName() )
                    {
                        Logger.Info( "match" );
                    }
                    else
                    {
                        if ( qQ3.ToString() == q2.GetName().ToString() )
                        {
                            Logger.Warn( $"match2 {qQ3} {q2}" );
                        }

                        if ( AssemblyName.ReferenceMatchesDefinition( qQ3, q2.GetName() ) )
                        {
                            Logger.Warn( $"error match {qQ3} {q2}" );
                        }
                    }
                }
            }


            var r = q.Where( assembly => Attribute.IsDefined( assembly, typeof(WpfTestApplicationAttribute) ) );
            Assert.NotEmpty( q );

            var theAssembly = q.First();
            _wpfApplicationHelper = new WpfApplicationHelper( theAssembly );
        }
#endif
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _wpfApplicationHelper?.Dispose();
        }
    }
}