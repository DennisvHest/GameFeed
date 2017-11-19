using System;

namespace GameFeed.Common.Exceptions {
    public class GameFeedException : Exception {

        public GameFeedException(string message) : base(message) { }
        public GameFeedException(string message, Exception inner) : base(message, inner) { }
    }
}
