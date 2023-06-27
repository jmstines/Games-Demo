<template>
  <v-toolbar density="compact">
    <v-menu location="bottom">
      <template v-slot:activator="{ props }">
        <v-btn
          icon
          color="primary"
          dark
          v-bind="props"
        >
          <v-app-bar-nav-icon></v-app-bar-nav-icon>
        </v-btn>
      </template>

      <v-list>
        <v-list-item
          v-for="(item, index) in items"
          :key="index"
          @click="$router.push({ path: item.link})"
        >
          <v-list-item-title>{{ item.title }}</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>
    

    <v-toolbar-title>Stines Solutions</v-toolbar-title>

    <v-spacer></v-spacer>

    <v-btn icon>
      <v-icon>mdi-magnify</v-icon>
    </v-btn>

    <v-btn
      icon
      color="primary"
      dark
    >
      <v-icon>mdi-dots-vertical</v-icon>
    </v-btn>
  </v-toolbar>
</template>

<script lang="ts" setup>


</script>

<script lang="ts">
import { defineComponent } from "vue"
  export default defineComponent({
    name: 'ToolbarDefault',
    props: {
      title: String
    },
    data: () => ({
      items: [
        { title: "Home", link: '/'},
        { title: 'Blackjack', link: '/blackjack'}
      ],
    })
  })

</script>