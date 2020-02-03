using System.Collections.Generic ;
using System.Runtime.Serialization ;
using AppShared ;
using AppShared.Infos ;

namespace TestLib.Fixtures
{
    public class ObjectIDFixture
    {
        public delegate long GetObjectIdDelegate ( object obj , out bool firstTime ) ;

        private readonly IDictionary < long , object >
            id_obj = new Dictionary < long , object > ( ) ;

        private readonly IDictionary < object , long >
            obj_id = new Dictionary < object , long > ( ) ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public ObjectIDFixture ( )
        {
            Generator       = new ObjectIDGenerator ( ) ;
            InstanceFactory = new InfoContext.Factory ( Generator ) ;
            GetObjectId     = _GetObjectId ;
        }

        public ObjectIDGenerator Generator { get ; }

        public GetObjectIdDelegate GetObjectId { get ; }


        public InfoContext.Factory InstanceFactory { get ; }

        private long _GetObjectId ( object obj , out bool firstTime )
        {
            var id = Generator.GetId ( obj , out firstTime ) ;
            if ( firstTime )
            {
                obj_id[ obj ] = id ;
                id_obj[ id ]  = obj ;
            }

            return id ;
        }
    }
}