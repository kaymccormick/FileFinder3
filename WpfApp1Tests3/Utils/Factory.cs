using System.Runtime.Serialization;
using WpfApp1.Util ;

namespace WpfApp1Tests3.Utils
{
    public class Factory
    {
        private readonly ObjectIDGenerator _generator;

        public Factory(
            ObjectIDGenerator generator
        )
        {
            _generator = generator;
        }

        public ContextStack<T> CreateContextStack<T>() where T : InfoContext
        {
            var contextStack = new ContextStack<T>();
            Register( contextStack );
            return contextStack;
        }

        private void Register < TToRegister >(
            TToRegister instance
        )
        {
            bool firstTime;
            _generator.GetId( instance, out firstTime );
        }

        public ContextStack<T> CreateContextStack<T>(bool allowDuplicateNames) where T : InfoContext
        {
            var contextStack = new ContextStack<T>(allowDuplicateNames);
            Register(contextStack);
            return contextStack;
        }

    }
}
