// Define some routes
// Each route should map to a component.
// We'll talk about nested routes later.
const routes=[
    {path: '/home', component:home},
    {path: '/employee', component:employee},
    {path: '/department', component:department},
]

// Create the router instance and pass the `routes` option
// You can pass in additional options here, but let's
// keep it simple for now.
const router = new VueRouter.createRouter({
    // Provide the history implementation to use. We are using the hash history for simplicity here.
    history: VueRouter.createWebHashHistory(),
    routes  // short for `routes: routes`
})

// Create and mount the root instance.
const app = Vue.createApp({})
// Make sure to _use_ the router instance to make the
// whole app router-aware.
app.use(router)

app.mount('#app')
// Now the app has started!