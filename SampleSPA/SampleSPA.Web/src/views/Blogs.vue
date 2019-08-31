<template>
  <div class="blogs">
    <wait-cursor
      message="Getting Blogs ..."
      :busy="isBusy"
    />
    <h1>This is a {{ appName }}</h1>
    <button
      id="getBlogs"
      @click="getBlogs()"
    >
      Get Blogs
    </button>
    <form @submit.prevent="onSave()">
      <div class="form-group">
        <label>Url</label>
        <input
          id="newBlogUrl"
          class="form-control"
          v-model="newBlog.url"
          name="newBlogUrl"
          v-validate="{ required: true }"
        >
        <span class="text-danger" />
      </div>
      <div class="form-group">
        <input
          id="saveBlog"
          type="submit"
          class="btn"
          value="Save"
        >
      </div>
    </form>
    <h3>{{ error }}</h3>
    <h3>Blogs ...</h3>
    <ul>
      <li
        v-for="blog in blogs"
        :key="blog.id"
      >
        {{ blog.url }}
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios'
import Vue from 'vue'
import VeeValidate from 'vee-validate'
import WaitCursor from '@/components/WaitCursor.vue'

Vue.use(VeeValidate)

export default {
  name: 'Blogs',
  components: {
    WaitCursor
  },
  data () {
    return {
      appName: 'Blogs page',
      error: '',
      isBusy: false,
      blogs: [],
      newBlog: {
        url: ''
      }
    }
  },
  methods: {
    onSave: function () {
      this.error = ''
      this.isBusy = true
      axios.post('http://localhost:5000/api/blogs', this.newBlog)
        .then((response) => {
          this.isBusy = false
          this.blogs = response.data
        }, (error) => {
          this.isBusy = false
          this.error = error.response.statusText + ':' + error.response.data
        })
    },

    getBlogs: function () {
      this.error = ''
      this.isBusy = true
      axios.get('http://localhost:5000/api/blogs')
        .then((response) => {
          this.isBusy = false
          this.blogs = response.data
        }, (error) => {
          this.isBusy = false
          this.error = error
        })
    }
  },
  mounted () {
  },
  computed: {
  }
}
</script>
