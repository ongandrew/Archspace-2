using System;

namespace Archspace2.Battle.Simulator
{
    public class Simulator
    {
        public Configuration Configuration { get; protected set; }

        public Simulator(Action<ConfigurationBuilder> configureBuilder)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            configureBuilder.Invoke(builder);
            Configuration = builder.Build();
        }

        public Simulator(Configuration configuration)
        {
            Configuration = configuration;
        }

        public Simulation CreateSimulation()
        {
            return new Simulation(Configuration);
        }
    }

    
}
