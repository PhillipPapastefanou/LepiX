using System;

namespace LepiX.Core
{
    class RUN
    {
        //------------------------------------------------------------
        // variables 
        //------------------------------------------------------------
        int pID;

        Component[] components;


        //------------------------------------------------------------
        // properties 
        //------------------------------------------------------------
        public Component[] Components
        {
            get { return components; }
            set { components = value; }
        }

        public int PID
        { get { return pID; } }


        //------------------------------------------------------------
        // constructor 
        //------------------------------------------------------------
        public RUN(int pID)
        {
            this.pID = pID;
            components = new Component[5];  
        }

        //------------------------------------------------------------
        // methods 
        //------------------------------------------------------------
        public void Initialise(Scenario scenario)
        {
            foreach (Component component in components)
            {
                component.Initialise(scenario);
            }
        }


        // main model loop 
        public void Simulate()
        {
            foreach (Component component in components)
            {
                component.Input();

                component.Update();

                component.Output();
            }
        }

        public void Dispose()
        {
            foreach (Component component in components)
            {
                component.Dispose();
            }
        }


    }
}
