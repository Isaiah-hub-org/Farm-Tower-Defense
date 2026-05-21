shader_type canvas_item;

uniform sampler2D noise_texture : filter_nearest, repeat_enable;

uniform vec4 color_start : source_color = vec4(0.05, 0.05, 0.05, 0.55);
uniform vec4 color_end : source_color = vec4(0.75, 0.75, 0.75, 0.55);
uniform vec4 edge_color : source_color = vec4(1.0, 0.55, 0.05, 1.0);

uniform float opacity : hint_range(0.0, 1.0) = 0.65;

uniform float gradient_width : hint_range(0.05, 2.0) = 0.45;
uniform float fade_position : hint_range(-1.0, 2.0) = 0.0;
uniform float fade_speed : hint_range(0.0, 5.0) = 0.45;

uniform float noise_scale : hint_range(1.0, 100.0) = 35.0;
uniform float noise_strength : hint_range(0.0, 1.0) = 0.35;

uniform float edge_size : hint_range(0.001, 0.2) = 0.06;

uniform vec2 move_direction = vec2(1.0, 0.0);

void fragment() {
	vec2 uv = UV;
	vec4 base = texture(TEXTURE, uv);

	vec2 dir = normalize(move_direction);

	// Animated position moving across the sprite
	float animated_pos = fade_position + TIME * fade_speed;

	// This makes the wipe repeat forever
	animated_pos = mod(animated_pos, 2.0) - 0.5;

	// Directional gradient across the sprite
	float gradient = dot(uv, dir);

	// Moving noise so the dissolve looks gravelly
	vec2 noise_uv = uv * noise_scale;
	noise_uv += dir * TIME * fade_speed;

	float noise = texture(noise_texture, noise_uv).r;

	// Add rough gravel breakup to the gradient
	float dissolve_line = gradient + (noise - 0.5) * noise_strength;

	// Fade mask
	float fade = smoothstep(
		animated_pos,
		animated_pos + gradient_width,
		dissolve_line
	);

	// Dissolve edge glow
	float edge = smoothstep(
		animated_pos,
		animated_pos + edge_size,
		dissolve_line
	) - smoothstep(
		animated_pos + edge_size,
		animated_pos + edge_size * 2.0,
		dissolve_line
	);

	// Gradient color
	vec4 gradient_color = mix(color_start, color_end, gradient);

	vec3 final_rgb = base.rgb * gradient_color.rgb;
	final_rgb = mix(final_rgb, edge_color.rgb, edge);

	COLOR.rgb = final_rgb;
	COLOR.a = base.a * fade * opacity;
}