<template>
  <div class="hello">
    <h1 class="myMessage">
      {{ msg }}
    </h1>
    <a @click="handleClick">Click me 9!</a>
  </div>
</template>

<script>

export default {
  name: 'HelloWorld',
  props: {
    msg: {
      type: String,
      default: ''
    }
  },
  methods: {
    handleClick: function () {
      alert('test2')
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

h1 {
  color: #42b983;
}
h3 {
  margin: 30px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
