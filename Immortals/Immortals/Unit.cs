using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Immortals
{
    /// <summary>
    /// Class to represent a tactical unit</summary>
    class Unit
    {
        // the list of models for the unit
        List<BasicModel> modelList;

        // the game stats
        int Mv;

        // aesthetic data
        String name;

        // Parent pointer
        ImmortalsEngine engine;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="engine"> Pointer to the parent object.</param>
        /// <param name="Mv"> The Mv stat for the object.</param>
        /// <param name="name"> The name of this unit.</param>
        public Unit(ImmortalsEngine engine, int Mv, String name)
        {
            // Store data
            this.engine = engine;
            this.Mv = Mv;
            this.name = name;
        }
    }
}
