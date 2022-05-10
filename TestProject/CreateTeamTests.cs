using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.GraphApi.BetaTest;
using NUnit.Framework;

namespace TestProject
{
    public class CreateTeamTests
    {
        [Test]
        public async Task CreateTeam()
        {
            var createTeam = new CreateTeam();

            await createTeam.Create();
        }
    }
}
