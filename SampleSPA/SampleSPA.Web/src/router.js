import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import NotFound from './views/NotFound.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/blogs',
      name: 'blogs',
      // route level code-splitting
      // this generates a separate chunk (products.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "blogs" */ './views/Blogs.vue')
    },
    {
      path: '*',
      name: 'notfound',
      component: NotFound
    }
  ]
})
