<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" version="1.0" encoding="utf-8"/>  
  <xsl:template match="/">
    <xsl:element name="xsl:stylesheet">
      <xsl:attribute name="version">
        <xsl:text>2.0</xsl:text>
      </xsl:attribute>
      <xsl:element name="xsl:template">        
        <xsl:attribute name="match">
          <xsl:text>/</xsl:text>
        </xsl:attribute>        
        <xsl:element name="MDTData">
          <xsl:element name="xsl:apply-templates">
          </xsl:element>
        </xsl:element>
      </xsl:element>
      <xsl:apply-templates  select="//Table"></xsl:apply-templates>
      <!--<xsl:element name="xsl:template">
        <xsl:attribute name="match">
          <xsl:text>@* | node()</xsl:text>
        </xsl:attribute>
        <xsl:element name="xsl:copy">
          <xsl:element name="xsl:apply-templates">
            <xsl:attribute name="select">
              <xsl:text>@* | node()</xsl:text>
            </xsl:attribute>
          </xsl:element>
        </xsl:element>
      </xsl:element>-->
    </xsl:element>
  </xsl:template>
  <xsl:template match="Table">
    <xsl:element name="xsl:template">
      <xsl:attribute name="match">
        <xsl:value-of select="@Source"/>
      </xsl:attribute>
        <xsl:element name="{@Target}">
          <xsl:apply-templates select="Column"></xsl:apply-templates>
        </xsl:element>
      </xsl:element>
  </xsl:template>
  <xsl:template match="Column">
    <xsl:if test="@Value">
      <xsl:element name="{@Target}">
         <xsl:value-of select="@Value"/>
      </xsl:element>
    </xsl:if>
    <xsl:if test="@Source">
      <xsl:element name="xsl:if">
        <xsl:attribute name="test">
          <xsl:value-of select="@Source"/>
        </xsl:attribute>
        <xsl:element name="{@Target}">
          <xsl:element name="xsl:value-of">
            <xsl:attribute name="select">
              <xsl:value-of select="@Source"/>
            </xsl:attribute>
          </xsl:element>
        </xsl:element>
      </xsl:element>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>
