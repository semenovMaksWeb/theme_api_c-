namespace api.Sql
{
    public class SqlCommand
    {
        static public Dictionary<string, string> sqlTheme = new Dictionary<string, string>()
        {
            ["getAll"] = @"
                select id, name,description
                from public.theme
            ",
            ["getPages"] = @"
                select id, name,description
                from public.theme
                LIMIT @limit OFFSET @offset;
            ",
            ["getNameWhereId"] = @"
                SELECT id, name,description count FROM public.theme WHERE id=@id",
            ["getCountWhereName"] = @"
                SELECT count(*) count FROM public.theme WHERE name=@name",
            ["getCountWhereNameAndId"] = @"
                SELECT count(*) count FROM public.theme WHERE name=@name and id<>@id",
            ["getCountWhereId"] = @"
                SELECT count(*) count FROM public.theme WHERE id=@id",
            ["updateDescription"] = @"
                UPDATE public.theme 
                SET description=@description WHERE id=@id;
            ",
            ["updateAll"] = @"
                 UPDATE public.theme
                SET name=@name, description=@description WHERE id=@id;",
            ["delete"] = @"
                 DELETE FROM public.theme WHERE id=@id;",
            ["deleteIn"] = @"
                 DELETE FROM public.theme WHERE id = ANY (@ids);",
            ["save"] = @"
                 INSERT INTO public.theme (name, description) VALUES(@name, @description) RETURNING id",
        };
        static public Dictionary<string, string> sqlVarCssNameTheme = new Dictionary<string, string>()
        {

            ["getInsertVar"] = @"
                SELECT * FROM var_css_name where  id not in (select id_var_css_name from var_css_name__theme vcnt where id_theme  = @id_theme)
            ",
            ["insertAll"] = @"
                insert into var_css_name__theme (id_theme, id_var_css_name)
                values(
	                @id_theme,
	                 unnest(array(select id from var_css_name))
                )
            ",
            ["insertAllVarCss"] = @"
                insert into var_css_name__theme (id_theme, id_var_css_name)
                values(
                 unnest(array(select id from theme)),
                @id_var_css
                )
            ",
            ["deleteThemeId"] = @"
                DELETE FROM var_css_name__theme WHERE id_theme = @id_theme;
            ",
            ["deleteVarCssId"] = @"
                DELETE FROM var_css_name__theme WHERE id_var_css_name = @id_var_css;
            ",

            ["deleteVarCssIds"] = @"
                DELETE FROM var_css_name__theme WHERE id_var_css_name = ANY(@ids_var_css);
            ",
            ["deleteThemeIds"] = @"
                DELETE FROM var_css_name__theme WHERE id_theme = ANY(@ids_theme);
            ",
            ["delete"] = "DELETE FROM var_css_name__theme WHERE id = @id;",
            ["save"] = @"
                insert into var_css_name__theme (id_theme, id_var_css_name)
                values(
                    @id_theme,
                    @id_var_css
                ) RETURNING id;
            ",
            ["updateResetThemeId"] = @"
                UPDATE public.var_css_name__theme vsnt
                SET value =  null  where vsnt.id_theme  = @id_theme
            ",
            ["updateAll"] = @"
                     update var_css_name__theme as vcn 
                    set value = c.value
                    from (values
                       @values
                    ) as c(id, value) 
                    where c.id = vcn.id;
            ",
            ["updateResetThemeIdAndVarId"] = @"
                UPDATE public.var_css_name__theme vsnt
                SET value =  null  where vsnt.id_theme  = @id_theme and vsnt.id_var_css_name = @id_var
            ",
            ["getThemeId"] = @"
                select vcnt.id as id,vcm.name  as var_name, value
                from var_css_name__theme vcnt
                left join theme  on vcnt.id_theme = theme.id
                left join var_css_name vcm on vcm.id = vcnt.id_var_css_name
                where theme.id = @id_theme
            ",
        };
        static public Dictionary<string, string> sqlVarCssName = new Dictionary<string, string>()
         {
             ["getAll"] = @"
                select id, name,description 
                    from public.var_css_name",
            ["getId"] = @"
                select id, name,description 
                    from public.var_css_name where id=@id;",
            ["getCountWhereName"] = @"
                SELECT count(*) count FROM public.var_css_name WHERE name=@name",
             ["getCountWhereId"] = @"
                SELECT count(*) count FROM public.var_css_name WHERE id=@id",
            ["getCountWhereNameAndId"] = @"
                SELECT count(*) count FROM public.var_css_name WHERE name=@name and id<>@id",
            ["save"] = @"
                 INSERT INTO public.var_css_name (name, description) VALUES(@name, @description) RETURNING id",
             ["delete"] = @"
                 DELETE FROM public.var_css_name WHERE id=@id;",
             ["deleteIn"] = @"
                 DELETE FROM public.var_css_name WHERE id = ANY (@ids);",
             ["updateAll"] = @"
                 UPDATE public.var_css_name 
                SET name=@name, description=@description WHERE id=@id;",
           ["updateDescription"] = @"
                 UPDATE public.var_css_name 
                SET description=@description WHERE id=@id;"
         };     
    }
}
