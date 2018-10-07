using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2.Battle.Simulator
{
    public class Simulator
    {
        public Configuration Configuration { get; protected set; }

        public Simulator()
        {
            Configuration = new Configuration();
        }

        public Simulator(Action<ConfigurationBuilder> configureBuilder)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            configureBuilder.Invoke(builder);
            Configuration = builder.Build();
        }

        public Simulation CreateSimulation()
        {
            return new Simulation(Configuration);
        }
    }

    
}
