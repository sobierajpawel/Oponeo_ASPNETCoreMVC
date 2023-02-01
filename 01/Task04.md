## Task 4

### Creating web application with onion architecture

1. Write your own onion architecture with `Domain`, `Persistence`, `Application`.

2. Move model into `Domain` project. Add interfaces of repositories into that project.

3. In the `Application` project add services to make CRUD operations with using repositories.

4. In the `Persistence` layer make implementation of interfaces.

5. Change controllers to using service from `Application` layer.
