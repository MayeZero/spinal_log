using UnityEngine;

public class LowPass
{
    private readonly float[] a;
    private readonly float[] b;
    private readonly float omega0;
    private float dt;
    private readonly bool adapt;
    private float tn1 = 0;
    private readonly float[] x;
    private readonly float[] y;

    public LowPass(float f0, float fs, bool adaptive, int order)
    {
        omega0 = 6.28318530718f * f0;
        dt = 1.0f / fs;
        adapt = adaptive;
        tn1 = -dt;
        x = new float[order + 1];
        y = new float[order + 1];
        a = new float[order];
        b = new float[order + 1];
        setCoef(order);
    }

    private void setCoef(int order)
    {
        if (adapt)
        {
            float t = Time.realtimeSinceStartup;
            dt = t - tn1;
            tn1 = t;
        }

        float alpha = omega0 * dt;
        if (order == 1)
        {
            a[0] = -(alpha - 2.0f) / (alpha + 2.0f);
            b[0] = alpha / (alpha + 2.0f);
            b[1] = alpha / (alpha + 2.0f);
        }
        else if (order == 2)
        {
            float alphaSq = alpha * alpha;
            float[] beta = { 1, Mathf.Sqrt(2), 1 };
            float D = alphaSq * beta[0] + 2 * alpha * beta[1] + 4 * beta[2];
            b[0] = alphaSq / D;
            b[1] = 2 * b[0];
            b[2] = b[0];
            a[0] = -(2 * alphaSq * beta[0] - 8 * beta[2]) / D;
            a[1] = -(beta[0] * alphaSq - 2 * beta[1] * alpha + 4 * beta[2]) / D;
        }
    }

    public float Filt(float xn, int order)
    {
        if (adapt)
        {
            setCoef(order);
        }
        y[0] = 0;
        x[0] = xn;
        for (int k = 0; k < order; k++)
        {
            y[0] += a[k] * y[k + 1] + b[k] * x[k];
        }
        y[0] += b[order] * x[order];
        for (int k = order; k > 0; k--)
        {
            y[k] = y[k - 1];
            x[k] = x[k - 1];
        }
        return y[0];
    }
}