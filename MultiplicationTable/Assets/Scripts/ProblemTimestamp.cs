public class ProblemTimestamp
{
    private float time;
    private ProblemTimestampType type;

    ProblemTimestamp(float c_time, ProblemTimestampType c_type){
        time = c_time;
        type = c_type;
    }
}

public enum ProblemTimestampType
{
    Success,
    Mistake,
    ShowHelp,
    CloseHelp
}