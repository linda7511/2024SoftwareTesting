<template>
  <test-panel :context="context" :options="options" :code="code" :versions="versions" :ec-option="ecOption"
    :iteration="iteration">
    <template #header>
      Question 04. 电信收费问题
    </template>
    <template #sub-title>
      算法思想
    </template>
    <template #detail>
      本问题输入变量为本月通话时间、用户本年度未按时缴费次数两个。
      <br />① 首先判断本月通话时长是否符合取值范围，通话时间应该≥0，且不能超过一个月的分钟数，这里使用31*24*60=44640作为最大值；
      <br />② 之后还需要判断用户本年度未按时缴费次数是否符合取值范围，显然次数应该≥0，同时在本月之前本年度最多有11个月缴费，所以未按时缴费次数≤11；
      <br />③ 计算用户本月的通话时长处于哪一个折扣等级；
      <br />④ 计算用户本年度未按时缴费次数是否超出当前折扣等级容许的未按时缴费次数。
      <br />⑤ 通过折后费用加上月租费计算出最终的本月通话费用。
    </template>
  </test-panel>
</template>

<script setup lang="ts">
import * as echarts from 'echarts/core'
import TestPanel from '../../components/TestPanel.vue'
import type { ECOption } from '@/interface'

// 上下文
const context = 'telecomSystem'

// 测试用例集选项
const options = [
  {
    label: '边界值',
    value: 'boundary-value',
    children: [
      {
        label: '基本边界值',
        value: 'basic-boundary',
      },
      {
        label: '健壮边界值',
        value: 'robustness-boundary',
      },
      {
        label: '最坏边界值',
        value: 'worst-boundary',
      },
    ]
  },
  {
    label: '等价类',
    value: 'equivalence',
    children: [
      {
        label: '弱一般等价类',
        value: 'weak-general-equivalent',
      },
      {
        label: '强一般等价类',
        value: 'strong-general-equivalent',
      },
      {
        label: '弱健壮等价类',
        value: 'weak-robustness-equivalent',
      },
      {
        label: '强健壮等价类',
        value: 'strong-robustness-equivalent',
      },
    ]
  },
  {
    label: '决策表',
    value: 'decision',
    children: [
      {
        label: '决策表',
        value: 'decision-table',
      }
    ]
  }
]

// 实现代码
const code = `function tele_toll_system(t, n) {
    if(t<0||n<0||t>44640||n>11)
        return "数值越界";
    if(t>=0&&t<=60){
        var result= n<=1?25+0.15*t*(1-0.01):25+0.15*t;
        return result.toFixed(2);
    }
    if(t>60&&t<=120){
        var result= n<=2?25+0.15*t*(1-0.015):25+0.15*t;
        return result.toFixed(2);
    }
    if(t>120&&t<=180){
        var result= n<=3?25+0.15*t*(1-0.02):25+0.15*t;
        return result.toFixed(2);
    }
    if(t>180&&t<=300){
        var result= n<=3?25+0.15*t*(1-0.025):25+0.15*t;
        return result.toFixed(2);
    }
    if(t>300){
        var result= n<=6?25+0.15*t*(1-0.03):25+0.15*t;
        return result.toFixed(2);
    }
}`

// 程序版本集
const versions = [
  {
    label: '0.0.0',
    value: '0.0.0'
  },
  {
    label: '0.1.0',
    value: '0.1.0'
  },
  {
    label: '0.2.0',
    value: '0.2.0'
  },
]

// ECharts 绘图选项
const ecOption: ECOption = {
  xAxis: {
    type: 'category',
    data: ['v0.1.0边界值','等价类','决策表','最优表', 'v0.1.0边界值','等价类','决策表','最优表']
  },
  yAxis: [
    {
      type: 'value',
      name: '测试用例通过率',
      alignTicks: true,
      position: 'left',
      axisLabel: {
        formatter: '{value} %'
      }
    },
    {
      type: 'value',
      name: '测试用例通过数',
      position: 'right',
    }
  ],
  tooltip: {
    trigger: 'axis'
  },
  toolbox: {
    show: true,
    feature: {
      dataView: { show: true, readOnly: false },
      magicType: { show: true, type: ['line', 'bar'] },
      restore: { show: true },
      saveAsImage: { show: true }
    }
  },
  series: [
    {
      data: [
      {
          value: 63.46,//
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#83bff6" },
              { offset: 0.8, color: "#188df0" },
              { offset: 1, color: "#188df0" },
            ]),
          }
        },
        {
          value: 67.57,//
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#31B7D9" },
              { offset: 0.8, color: "#70C6DB" },
              { offset: 1, color: "#A3CED9" },
            ]),
          }
        },
        {
          value: 45.45,//
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#8F72A3" },
              { offset: 0.8, color: "#7C44A5" },
              { offset: 1, color: "#6B18A6" },
            ]),
          }
        },
        {
          value: 32.35,//
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#76769A" },
              { offset: 0.8, color: "#4E4F9F" },
              { offset: 1, color: "#383AA5" },
            ]),
          }
        },
        {
          value: 100,
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#83bff6" },
              { offset: 0.8, color: "#188df0" },
              { offset: 1, color: "#188df0" },
            ]),
          }
        },
        {
          value: 100,//
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#31B7D9" },
              { offset: 0.8, color: "#70C6DB" },
              { offset: 1, color: "#A3CED9" },
            ]),
          }
        },
        {
          value: 100,//
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#8F72A3" },
              { offset: 0.8, color: "#7C44A5" },
              { offset: 1, color: "#6B18A6" },
            ]),
          }
        },
        {
          value: 100,//
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
            { offset: 0, color: "#76769A" },
              { offset: 0.8, color: "#4E4F9F" },
              { offset: 1, color: "#383AA5" },
            ]),
          }
        },
      ],
      type: 'bar',
      yAxisIndex: 0,
      name: '测试用例通过率',
      tooltip: {
        valueFormatter: (value) => value + ' %'
      }
    },
    // {
    //   data: [
    //     {
    //       value: 39,
    //       itemStyle: {
    //         color: 'green'
    //       }
    //     },
    //     {
    //       value: 46,
    //       itemStyle: {
    //         color: 'green'
    //       }
    //     },
    //     {
    //       value: 49,
    //       itemStyle: {
    //         color: 'green'
    //       }
    //     }
    //   ],
    //   type: 'line',
    //   yAxisIndex: 1,
    //   markPoint: {
    //     data: [
    //       { type: 'max', name: 'Max' }
    //     ]
    //   },
    //   name: '测试用例通过数',
    //   tooltip: {
    //     valueFormatter: (value) => value + ' 个'
    //   }
    // }
  ]
}

// 代码版本迭代信息
const iteration = {
  columns: [{
    title: '版本号',
    key: 'version'
  }, {
    title: '测试数据集',
    key: 'dataset'
  }, {
    title: '测试情况',
    key: 'result'
  }, {
    title: '缺陷描述',
    key: 'bug'
  }],
  data: [{
    key: '0',
    version: '0.0.0',
    dataset: '边界值',
    result: '通过33/52',
    bug: '折扣率计算错误'
  }, {
    key: '1',
    version: '0.0.0',
    dataset: '等价类',
    result: '通过39/41',
    bug: '折扣率计算错误'
  }, {
    key: '2',
    version: '0.0.0',
    dataset: '决策表',
    result: '通过5/11',
    bug: '折扣率计算错误'
  },{
    key: '3',
    version: '0.0.0',
    dataset: '最优测试用例',
    result: '通过22/68',
    bug: '折扣率计算错误'
  },
  {
    key: '4',
    version: '0.1.0',
    dataset: '边界值',
    result: '通过52/52',
    bug: '测试全部通过'
  }, {
    key: '5',
    version: '0.1.0',
    dataset: '等价类',
    result: '通过41/41',
    bug: '测试全部通过'
  }, {
    key: '6',
    version: '0.1.0',
    dataset: '决策表',
    result: '通过11/11',
    bug: '测试全部通过'
  },{
    key: '72',
    version: '0.1.0',
    dataset: '最优测试用例',
    result: '通过68/68',
    bug: '测试全部通过'
  },]
}
</script>

<style scoped>
</style>