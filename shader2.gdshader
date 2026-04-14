shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;
uniform sampler2D noise_texture : filter_nearest, repeat_enable;

uniform float wave_speed = 0.45;
uniform float noise_scale = 7.0;
uniform float distortion_strength = 0.035;
uniform vec4 water_tint : source_color = vec4(0.2, 0.55, 1.0, 0.12);

void fragment() {
    vec2 noise_uv = UV * noise_scale;

    float n1 = texture(noise_texture, noise_uv + vec2(TIME * wave_speed, 0.0)).r;
    float n2 = texture(noise_texture, noise_uv + vec2(0.0, TIME * wave_speed * 0.8)).r;
    float n3 = texture(noise_texture, noise_uv - vec2(TIME * wave_speed * 0.6, TIME * wave_speed * 0.4)).r;

    vec2 offset = vec2(
        (n1 - 0.5) + (n3 - 0.5) * 0.6,
        (n2 - 0.5) + (n3 - 0.5) * 0.6
    ) * distortion_strength;

    vec4 bg = texture(screen_texture, SCREEN_UV + offset);

    bg.rgb = mix(bg.rgb, water_tint.rgb, water_tint.a);

    COLOR = bg;
}