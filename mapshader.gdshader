shader_type canvas_item;

uniform sampler2D noise_texture: filter_nearest, repeat_enable;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

uniform float strength = 0.05;
uniform float speed = 1.0;

void fragment() {
    vec2 noiseUV = UV + vec2(TIME * speed, TIME * speed);
    float noise = texture(noise_texture, noiseUV).r;

    vec2 distortedUV = SCREEN_UV + (noise - 0.5) * strength;

    COLOR = texture(SCREEN_TEXTURE, distortedUV);
}