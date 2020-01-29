using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AppShared ;
using AutoFixture ;

namespace WpfApp1Tests3.Fixtures
{
    public class ObjectIDFixture
    {
        public delegate long GetObjectIdDelegate(
            object   obj,
            out bool firstTime
        );

        private IDictionary <object, long> obj_id = new Dictionary < object, long >();
        private IDictionary <long, object> id_obj = new Dictionary < long, object >();

        public ObjectIDGenerator Generator { get; }

        public GetObjectIdDelegate GetObjectId { get; }

        

        public Factory InstanceFactory { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ObjectIDFixture()
        {
            Generator = new ObjectIDGenerator();
            InstanceFactory = new Factory( Generator );
            GetObjectId = _GetObjectId;
        }

        private long _GetObjectId(
            object   obj,
            out bool firstTime
        )
        {
            long id = Generator.GetId(obj, out firstTime);
            if (firstTime)
            {
                obj_id[obj]  = id;
                id_obj[id] = obj;
            }

            return id;

        }
    }
}