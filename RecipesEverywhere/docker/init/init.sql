CREATE TABLE "Roles"
(
    "Id"   serial NOT NULL
        CONSTRAINT roles_pk PRIMARY KEY,
    "Name" text UNIQUE NOT NULL
);
INSERT INTO "Roles" ("Name")
VALUES 
    ('User'),
    ('Admin');

CREATE TABLE "Users"
(
    "Id"           serial NOT NULL
        CONSTRAINT users_pk PRIMARY KEY,
    "Name"         text UNIQUE NOT NULL,
    "PasswordHash" text NOT NULL,
    "RoleId"       integer NOT NULL
        CONSTRAINT users_roles_fk REFERENCES "Roles" ("Id")
);

CREATE TABLE "Statuses"
(
    "Id"   serial NOT NULL
        CONSTRAINT statuses_pk PRIMARY KEY,
    "Name" text NOT NULL UNIQUE
);

INSERT INTO "Statuses" ("Name")
VALUES 
    ('private'),
    ('public');

CREATE TABLE "Recipe"
(
    "Id"       serial NOT NULL
        CONSTRAINT recipes_pk PRIMARY KEY,
    "Title"    text NOT NULL,
    "Text"     text,
    "Picture"  text,
    "AuthorId" integer
        CONSTRAINT recipe_users_id_fk REFERENCES "Users" ("Id"),
    "StatusId" integer NOT NULL
        CONSTRAINT recipe_statuses_id_fk REFERENCES "Statuses" ("Id")
);

CREATE TABLE "Favourite"
(
    "Id"       serial NOT NULL
        CONSTRAINT favourite_pk PRIMARY KEY,
    "UserId"   integer NOT NULL
        CONSTRAINT favourite_user_fk REFERENCES "Users" ("Id"),
    "RecipeId" integer NOT NULL
        CONSTRAINT favourite_recipe_fk REFERENCES "Recipe" ("Id")
);

CREATE TABLE "Marks"
(
    "Id"     serial NOT NULL
        CONSTRAINT marks_pk PRIMARY KEY,
    "Mark"   integer NOT NULL,
    "RecipeId" integer NOT NULL
        CONSTRAINT marks_recipe_fk REFERENCES "Recipe" ("Id")
);
