shader_type canvas_item;

uniform sampler2D noise_texture : filter_linear, repeat_enable;

uniform vec4 base_color : source_color = vec4(0.15, 0.35, 1.0, 1.0);
uniform vec4 dissolve_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);

uniform float noise_scale_1 = 5.0;
uniform float noise_scale_2 = 12.0;
uniform float flow_speed = 0.18;

uniform float dissolve_position = 0.45;
uniform float dissolve_width = 0.20;
uniform float splatter_strength = 0.22;
uniform float speck_amount = 0.18;
uniform float speck_scale = 35.0;

uniform bool left_to_right = true;

void fragment() {
    vec2 uv = UV;

    float n1 = texture(noise_texture, uv * noise_scale_1 + vec2(TIME * flow_speed, 0.0)).r;
    float n2 = texture(noise_texture, uv * noise_scale_2 + vec2(-TIME * flow_speed * 0.6, TIME * flow_speed * 0.15)).r;

    float breakup = n1 * 0.65 + n2 * 0.35;

    float grad = left_to_right ? uv.x : (1.0 - uv.x);

    float dissolve_mask = smoothstep(
        dissolve_position - dissolve_width,
        dissolve_position + dissolve_width,
        grad + (breakup - 0.5) * splatter_strength
    );

    float speck_noise = texture(noise_texture, uv * speck_scale + vec2(TIME * 0.05, TIME * 0.02)).r;
    float specks = smoothstep(1.0 - speck_amount, 1.0, speck_noise);

    float edge_zone = 1.0 - abs(dissolve_mask - 0.5) * 2.0;
    edge_zone = clamp(edge_zone, 0.0, 1.0);

    vec3 col = mix(base_color.rgb, dissolve_color.rgb, dissolve_mask);
    col = mix(col, dissolve_color.rgb, specks * edge_zone * 0.9);

    // transparent fade
    float alpha = 1.0 - dissolve_mask;
    alpha += specks * edge_zone * 0.25;
    alpha = clamp(alpha, 0.0, 1.0);

    COLOR = vec4(col, alpha);
}