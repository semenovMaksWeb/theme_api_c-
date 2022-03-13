CREATE TABLE public.theme (
	id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
	"name" varchar NOT NULL,
	description varchar NULL,
	CONSTRAINT theme_pk PRIMARY KEY (id),
	CONSTRAINT theme_un UNIQUE (name)
);
CREATE TABLE public.var_css_name (
	id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
	"name" varchar NOT NULL,
	description varchar NULL,
	CONSTRAINT var_css_name_pk PRIMARY KEY (id),
	CONSTRAINT var_css_name_un UNIQUE (name)
);
CREATE TABLE public.var_css_name__theme (
	id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
	id_theme int4 NOT NULL,
	id_var_css_name int4 NOT NULL,
	value varchar NULL,
	CONSTRAINT var_css_name__theme_pk PRIMARY KEY (id),
	CONSTRAINT var_css_name__theme_un UNIQUE (id_theme, id_var_css_name),
	CONSTRAINT var_css_name__theme_fk FOREIGN KEY (id_theme) REFERENCES public.theme(id),
	CONSTRAINT var_css_name__theme_fk_1 FOREIGN KEY (id_var_css_name) REFERENCES public.var_css_name(id)
);


 