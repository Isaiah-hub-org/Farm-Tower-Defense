shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;

uniform float start_y: hint_range(0.0, 1.0) = .3;
uniform float strength: hint_range(0.0, 1.0) = .15;
uniform float curve_scale: hint_range(0.0, 3.0) = 1.5;
uniform float speed : hint_range(0.0, 10.0) = 2.0;
uniform float wind_start: hint_range(-1.57, 1.57) = 0.0;

varying vec4 v_color;

void vertex() {
	v_color = COLOR;

	float scale = (pow(2.0, curve_scale) * strength * 2.0);

	// Makes the ColorRect bigger so the wind distortion does not get cut off
	VERTEX *= 1.0 + scale;
}

void fragment() {
	float scale = (pow(2.0, curve_scale) * strength * 2.0);
	float total_scale = 1.0 + scale;

	// ColorRect uses plain UV
	vec2 local_uv = UV;

	vec2 adjusted_local_uv = (local_uv - 0.5) * total_scale + 0.5;

	vec2 wind_uv = adjusted_local_uv;

	wind_uv.x += sin(TIME * speed + wind_start)
		* smoothstep(start_y, 1.0 + start_y, pow((1.0 - wind_uv.y), 0.5))
		* pow((1.0 - wind_uv.y + 1.0), curve_scale)
		* strength;

	if (wind_uv.x < 0.0 || wind_uv.x > 1.0 || wind_uv.y < 0.0 || wind_uv.y > 1.0) {
		discard;
	}

	// Convert the fake UV movement into screen distortion
	vec2 wind_offset = wind_uv - local_uv;

	// Lower this if it is too strong
	vec2 final_screen_uv = SCREEN_UV + wind_offset * 0.05;

	// Sample whatever is behind the ColorRect
	vec4 screen_color = texture(screen_texture, final_screen_uv);

	// Output background only, so the ColorRect itself stays transparent
	COLOR = screen_color;
}