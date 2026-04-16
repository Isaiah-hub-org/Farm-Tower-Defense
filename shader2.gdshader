shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;
uniform sampler2D noise_texture : filter_nearest, repeat_enable;

uniform float wave_speed = 0.25;
uniform float noise_scale = 5.0;
uniform float distortion_strength = 0.004;
uniform float edge_softness = 0.18;
uniform float blob_scale = 1.8;

void fragment() {
    vec2 noise_uv = UV * noise_scale;

    float n1 = texture(noise_texture, noise_uv + vec2(TIME * wave_speed, 0.0)).r;
    float n2 = texture(noise_texture, noise_uv + vec2(0.0, TIME * wave_speed * 0.7)).r;
    float n3 = texture(noise_texture, noise_uv - vec2(TIME * wave_speed * 0.4, TIME * wave_speed * 0.3)).r;

    vec2 offset = vec2(
        (n1 - 0.5) + (n3 - 0.5) * 0.2,
        (n2 - 0.5) + (n3 - 0.5) * 0.2
    ) * distortion_strength;

    vec2 centered_uv = UV - vec2(0.5);
    float dist = length(centered_uv * blob_scale);

    float shape_noise = texture(noise_texture, UV * 2.0 + vec2(TIME * 0.08, TIME * 0.05)).r;
    float organic_edge = dist + (shape_noise - 0.5) * 0.18;

    float mask = 1.0 - smoothstep(0.32, 0.32 + edge_softness, organic_edge);

    vec4 bg = texture(screen_texture, SCREEN_UV + offset * mask);

    COLOR = vec4(bg.rgb, mask);
}