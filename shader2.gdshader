shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;

uniform vec4 water_tint : source_color = vec4(0.15, 0.45, 1.0, 0.18);

uniform vec2 ripple_center = vec2(0.5, 0.5);
uniform float ripple_speed = 4.0;
uniform float ripple_frequency = 55.0;
uniform float distortion_strength = 0.015;
uniform float wave_strength = 0.006;
uniform float edge_fade = 0.2;

void fragment() {
	vec2 uv = UV;
	vec2 screen_uv = SCREEN_UV;

	// Direction from ripple center
	vec2 dir = uv - ripple_center;
	float dist = length(dir);
	vec2 normal_dir = dist > 0.0001 ? normalize(dir) : vec2(0.0);

	// Circular ripple wave
	float ripple = sin(dist * ripple_frequency - TIME * ripple_speed);

	// Fade ripple out farther away
	float falloff = exp(-dist * 4.0);

	// Extra moving water wobble
	vec2 wave_offset = vec2(
		sin(uv.y * 24.0 + TIME * 1.8),
		cos(uv.x * 20.0 + TIME * 1.5)
	) * wave_strength;

	// Main distortion
	vec2 ripple_offset = normal_dir * ripple * falloff * distortion_strength;
	vec2 final_offset = ripple_offset + wave_offset;

	// Distort the screen behind the water
	vec4 distorted_screen = texture(screen_texture, screen_uv + final_offset);

	// Soft edge fade so it doesn't look like a hard box
	float left = smoothstep(0.0, edge_fade, uv.x);
	float right = smoothstep(0.0, edge_fade, 1.0 - uv.x);
	float top = smoothstep(0.0, edge_fade, uv.y);
	float bottom = smoothstep(0.0, edge_fade, 1.0 - uv.y);
	float edge_mask = left * right * top * bottom;

	// Final color
	vec3 final_rgb = mix(distorted_screen.rgb, water_tint.rgb, water_tint.a);
	float final_alpha = edge_mask * 0.9;

	COLOR = vec4(final_rgb, final_alpha);
}