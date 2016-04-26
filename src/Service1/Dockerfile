FROM microsoft/aspnet:1.0.0-rc1-update1
COPY project.json /app/
WORKDIR /app
RUN ["dnu", "restore"]
COPY . /app
ENTRYPOINT ["dnx", "run"]