using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8;
public class QuirkConfig
{
    public JumpOffsetBehaviour JumpOffsetBehaviour { get; set; } = JumpOffsetBehaviour.NNNPlusV0;
    public LoadStoreBehaviour LoadStoreBehaviour { get; set; } = LoadStoreBehaviour.Increment;
    public ShiftBehaviour ShiftBehaviour { get; set; } = ShiftBehaviour.Vx;

}

public enum LoadStoreBehaviour
{
    Increment,
    NoIncrement
}

public enum JumpOffsetBehaviour
{
    NNNPlusV0,
    XNNPlusVx
}

public enum ShiftBehaviour
{
    VxVy,
    Vx
}
