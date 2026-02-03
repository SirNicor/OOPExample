<template>
  <el-form inline = true class = "sort">
    <el-form-item label = "Сортировка по: ">
      <el-cascader :options="valueCascader" v-model = "sortType.sortKey"/>
    </el-form-item>
    <el-form-item label = "Тип сортировки: ">
      <el-cascader :options="OptionsSorted" v-model = "sortType.sortOrder"/>
    </el-form-item>
    <el-form-item>
      <el-button @click = "reset">reset</el-button>
    </el-form-item>
  </el-form>
  <el-table-v2
      :columns="CreateColumns"
      :data="CreateData"
      :width="viewportWidth"
      :height="viewportHeight*0.8"
      fixed
  />  
</template>
<style scoped>
  .sort {
    margin-top: 1%;
    margin-left:4px;
    padding-left: 4px;
    border: 1px solid;
    padding-top: 1%;
    margin-right: 35%;
  }
</style>  
<script lang="ts" setup>
  import type {TableColumn, TableData, SortType} from '@/types/TableTypes.ts';
  import {computed, onMounted, reactive} from "vue";
  import router from '@/router/index.ts';
  const sortType = reactive<SortType>({
    sortKey: '',
    sortOrder: '',
      });
  const viewportWidth = window.innerWidth;
  const viewportHeight = window.innerHeight;
  const props = defineProps <{
        columns: TableColumn[]
        data: TableData[]
        apiBase: string;
      }> ();
  const OptionsSorted = [
      {
    value: 'AscendingOrder',
    label: 'по Возрастанию',
      }, 
    {
      value: 'DescendingOrder',
      label: 'По убыванию',
    }];
  const valueCascader = computed(() =>
  {
    return props.columns.map((column) => {
      return {
        value: column.dataKey,
        label: column.title,
      }});
  })
  const CreateColumns = computed(() =>
  {
    return props.columns.map((column) => {
    return {
      ...column,
      width: column.width ?? 0.1 * viewportWidth
    }});
  })
  const CreateData = computed(() =>
  {
    if (!props.data || !props.columns) {
      return []
    }
    return props.data.map((datarow, keyForData) =>
    {
      const row: TableData  = {
        id: `${datarow.id || keyForData}`,
      }
      props.columns.forEach(((column, index) =>
      {
        if(datarow[column.dataKey] !== undefined)
        {
          row[column.dataKey] = datarow[column.dataKey];
        }
        else {
          const keys = Object.keys(datarow);
          if (keys.length > 0) {
            const dataIndex = index < keys.length ? index : 0;
            const key = keys[dataIndex];
            if(key && datarow[key] !== undefined)
            {
              row[column.dataKey] = datarow[key]
            }
          } else {  
            row[column.dataKey] = '';
          }
        }
      }))
      return row;
    })
  });
  const reset = (() =>
  {
    router.replace({query: {sortKey: sortType.sortKey, sortType: sortType.sortOrder}});
  })
</script> 