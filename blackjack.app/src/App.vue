<template>
  <v-app>
    <ToolbarDefault></ToolbarDefault>
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script lang="ts" setup>
import ToolbarDefault from '@/components/ToolbarDefualt.vue'

</script>