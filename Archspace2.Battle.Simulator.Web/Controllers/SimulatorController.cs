using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Universal.Common.Net.Http.Extensions;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator.Web
{
    [Route("simulator")]
    public class SimulatorController : Controller
    {
        protected Simulator mSimulator;

        public SimulatorController(Simulator simulator)
        {
            mSimulator = simulator;
        }

        [Route("create_simulation")]
        public IActionResult CreateSimulation()
        {
            return View();
        }

        [HttpPost]
        [Route("run_simulation")]
        public async Task<IActionResult> RunSimulation()
        {
            string body = await Request.Body.ReadAsStringAsync();

            RunSimulationRequest request = RunSimulationRequest.FromString(body);

            Simulation simulation = mSimulator.CreateSimulation()
                .SetBattlefield(new Battlefield() { Name = "Battlefield" })
                .SetAttacker(request.AttackingPlayer, request.AttackingArmada)
                .SetDefender(request.DefendingPlayer, request.DefendingArmada);

            Battle battle = simulation.Build();

            battle.Run();

            return new JsonResult(battle.Record);
        }

        internal class RunSimulationRequest : JsonSerializable<RunSimulationRequest>
        {
            [JsonProperty("AttackingPlayer")]
            public Player AttackingPlayer { get; protected set; }
            [JsonProperty("AttackingArmada")]
            public Armada AttackingArmada { get; protected set; }
            [JsonProperty("DefendingPlayer")]
            public Player DefendingPlayer { get; protected set; }
            [JsonProperty("DefendingArmada")]
            public Armada DefendingArmada { get; protected set; }
        }
    }
}