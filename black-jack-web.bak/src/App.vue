<template>
  <v-app>
    <v-app-bar app dark>
      <router-link to="/"> Home </router-link> |
      <router-link to="/about"> About </router-link> |
      <router-link to="/blackJack"> Black Jack </router-link>
    </v-app-bar>
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "App",
  components: {},
  data: () => ({})
});
</script>
