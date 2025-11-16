# Cómo traer los cambios al local

Los cambios recientes (incluido el botón de backup) ya están confirmados en la rama `work` del repositorio remoto. Para verlos en tu máquina:

1. **Abrí una terminal** en la carpeta donde clonaste el repo.
2. Ejecutá `git fetch` para traer los commits nuevos desde el remoto.
3. Si trabajás en la rama `work`, corré `git pull origin work` para actualizarla. Si tenés trabajo local sin confirmar, crea un commit o usa `git stash` antes de hacer el pull.
4. Una vez actualizado, abrí la solución `SIGACF.sln` con Visual Studio. El botón "Backup" estará disponible en `UI/MenuAdmin`.

> Tip: si querés revisar qué cambió antes de actualizar, usá `git log origin/work -5` o `git diff HEAD origin/work` para ver los últimos commits.
