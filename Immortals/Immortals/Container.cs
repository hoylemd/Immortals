using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Immortals
{
    /// <summary>
    /// Class to represent containers in a clicktree
    /// </summary>
    public abstract class Container
    {
        // rectangle which this container draws in in the frame of reference of it's parent
        protected Rectangle area;

        // list of child containers
        protected List<Container> children;

        /// <summary>
        /// Contructor
        /// </summary>
        public Container(Rectangle area)
        {
            // initialize members
            children = new List<Container>();

            // save data
            this.area = area;
        }

        /// <summary>
        /// Function to add a child container to this one.
        /// </summary>
        /// <param name="child">The child Container to add.</param>
        public void addChild(Container child)
        {
            this.children.Add(child);
        }

        /// <summary>
        /// Function to remove a child from this container.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        public void removeChild(Container child)
        {
            this.children.Remove(child);
        }

        /// <summary>
        /// Container behavior for clicking.  Decide on sub-member clicked and
        /// message.
        /// </summary>
        /// <param name="clickedPoint">
        /// Point object representing the location of the click in the parent's frame of reference.
        /// </param>
        public void Clicked(Point clickedPoint)
        {
            // normalize the point to this container's rectangle
            Point normalizedPoint = new Point(clickedPoint.X - area.X, clickedPoint.Y - area.Y);

            // validate the click point
            if (area.Contains(normalizedPoint))
            {
                // iterate through children and send click event to the first one the click is in.
                foreach (Container child in children)
                    if (child.area.Contains(normalizedPoint))
                    {
                        try
                        {
                            child.Clicked(normalizedPoint);
                            break;
                        }

                        // handle exceptions
                        catch (InvalidClickException ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    }
            }
            // throw exception on invalid click loacations
            else
                throw new InvalidClickException("click outside container boundaries");
        }
    }

}
