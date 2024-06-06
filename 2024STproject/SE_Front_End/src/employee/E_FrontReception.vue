<template>
    <div class="child-class">
        <router-view v-slot="props">
            <keep-alive>
                <component :is="props.Component"></component>
            </keep-alive>
        </router-view>
   </div>
</template>

<script>
</script>

<style>

</style>