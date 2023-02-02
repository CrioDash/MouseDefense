void invert_float(float4 Color, out float4 Out)
{
    Out = 1.0f - Color;
}

void invert_half(half4 Color, out half4 Out)
{
    Out = 1.0f - Color;
}