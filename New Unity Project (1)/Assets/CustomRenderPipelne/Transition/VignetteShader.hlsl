TEXTURE2D(_CameraColorTexture);
SAMPLER(sampler_CameraColorTexture);
float4 _CameraColorTexture_TexelSize;

void Original_float(float2 UV, out float4 Out) {
	float2 uvSamples[4];
	float depthSamples[4];
	float halfScaleFloor = floor(0 * 0.5);
	float halfScaleCeil = ceil(0 * 0.5);
	float2 Texel = (1.0) / float2(_CameraColorTexture_TexelSize.z, _CameraColorTexture_TexelSize.w);

	uvSamples[0] = UV - float2(Texel.x, Texel.y) * halfScaleFloor;
	uvSamples[1] = UV + float2(Texel.x, Texel.y) * halfScaleCeil;
	uvSamples[2] = UV + float2(Texel.x * halfScaleCeil, -Texel.y * halfScaleFloor);
	uvSamples[3] = UV + float2(-Texel.x * halfScaleFloor, Texel.y * halfScaleCeil);

	Out = SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, uvSamples[0]);
}
