using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point_ns;

namespace Node_ns
{
    public class Node
    {
        private Point crossing_pt;
        private int officeNumber;

        public Node(Point crossing_pt, int officeNumber)
        {
            this.crossing_pt = crossing_pt;
            this.officeNumber = officeNumber;
        }

        public Point getCrossingPt(){
            return crossing_pt;
        }

        public int getOfficeNumber(){
            return officeNumber;
        }

        public void setCrossingPt(Point crossing_pt)
        {
            this.crossing_pt = crossing_pt;
        }

        public void setOfficeNumber(int officeNumber)
        {
            this.officeNumber = officeNumber;
        }

    }
}
