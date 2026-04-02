shader_type canvas_item;

uniform sampler2D noise_texture: filter_nearest, repeat_enable;

uniform vec4 fog_color : source_color = vec4(0.85, 0.85, 0.85, 1.0);
uniform float speed = 0.15;
uniform float density = 2.5;
uniform float opacity = 0.7;
uniform float bottom_thickness = 0.7;

void fragment() {
    vec2 uv = UV;

    // layered noise (fixes square look 🔥)
    vec2 uv1 = uv * density + vec2(TIME * speed, 0.0);
    vec2 uv2 = uv * (density * 1.7) + vec2(-TIME * speed * 0.6, 0.0);

    float n1 = texture(noise_texture, uv1).r;
    float n2 = texture(noise_texture, uv2).r;

    float noise = (n1 + n2) * 0.5;

    // 🔥 THIS is what makes it ROUND instead of square
    float cloud = smoothstep(0.4, 0.8, noise);

    // fade from bottom
    float base = smoothstep(1.0, 1.0 - bottom_thickness, uv.y);

    float alpha = cloud * base * opacity;

    COLOR = vec4(fog_color.rgb, alpha);
}