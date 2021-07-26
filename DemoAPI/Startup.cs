using Owin;
using System;

namespace DemoAPI
{
    public class Startup
    {
        public static Guid RoomId = new Guid();
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

        }
    }
}