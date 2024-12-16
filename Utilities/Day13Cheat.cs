using Microsoft.Z3;

namespace Utilities;

public static class Day13Solver
{
    public static Vector? FindWinnersPart2(long aX, long aY, long bX, long bY, long pX, long pY)
    {
        List<Vector> results = new List<Vector>();

        Context ctx = new Context();
        Solver solver = ctx.MkSolver();

        var A = ctx.MkIntConst($"A");
        var B = ctx.MkIntConst($"B");

        var pax = ctx.MkInt(aX);
        var pay = ctx.MkInt(aY);

        var pbx = ctx.MkInt(bX);
        var pby = ctx.MkInt(bY);

        var resx = ctx.MkInt(pX);
        var resy = ctx.MkInt(pY);

        var X = ctx.MkAdd(ctx.MkMul(A, pax), ctx.MkMul(B, pbx));
        var Y = ctx.MkAdd(ctx.MkMul(A, pay), ctx.MkMul(B, pby));

        solver.Add(A >= 0);
        solver.Add(B >= 0);
        solver.Add(ctx.MkEq(X, resx));
        solver.Add(ctx.MkEq(Y, resy));

        Status statusVAl = solver.Check();

        if (statusVAl == Status.SATISFIABLE)
        {
            var model = solver.Model;

            var resA = Convert.ToInt64(model.Eval(A).ToString());
            var resB = Convert.ToInt64(model.Eval(B).ToString());

            Vector result = new Vector(resA, resB);
            return result;
        }


        return null;
    }
}