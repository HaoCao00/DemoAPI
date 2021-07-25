using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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